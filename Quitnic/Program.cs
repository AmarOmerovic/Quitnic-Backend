using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quitnic.Data;
using Quitnic.Repositories.Achievement.CoreAchievement;
using Quitnic.Repositories.Achievement.UserAchievement;
using Quitnic.Repositories.Base;
using Quitnic.Repositories.MotivationTip;
using Quitnic.Repositories.User;
using Quitnic.Repositories.UserSmokeHistory;
using Quitnic.Services.Achievement;
using Quitnic.Services.MotivationTip;
using Quitnic.Services.User;
using Quitnic.Services.UserSmokeHistory;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var supabaseConfig = builder.Configuration.GetSection("Supabase");
var supabaseUrl = supabaseConfig["Url"];
var supabaseProjectId = supabaseConfig["ProjectId"];
var jwtSecret = supabaseConfig["JwtSecret"];


if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseProjectId) || string.IsNullOrEmpty(jwtSecret))
{
    throw new InvalidOperationException("Required configuration values (SUPABASE_URL, SUPABASE_PROJECT_ID, JWT_SECRET) are missing.");
}
// Add services to the container

// Add DbContext and configure PostgreSQL connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JWT Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Authority = supabaseUrl;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = supabaseUrl,
            ValidAudience = supabaseProjectId,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

builder.Services.AddAuthorization();

// Base repository for all models
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

// Specific repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserSmokeHistoryRepository, UserSmokeHistoryRepository>();
builder.Services.AddScoped<IAchievementRepository, AchievementRepository>();
builder.Services.AddScoped<IUserAchievementRepository, UserAchievementRepository>();
builder.Services.AddScoped<IMotivationTipRepository, MotivationTipRepository>();

// Specific services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserSmokeHistoryService, UserSmokeHistoryService>();
builder.Services.AddScoped<IMotivationTipService, MotivationTipService>();
builder.Services.AddScoped<IAchievementService, AchievementService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular app URL
              .AllowAnyHeader()                    // Allow any headers
              .AllowAnyMethod();                   // Allow any HTTP methods (GET, POST, etc.)
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and your JWT token. Example: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthentication(); // Add Authentication Middleware
app.UseAuthorization();  // Add Authorization Middleware

app.MapControllers();

app.Run();
