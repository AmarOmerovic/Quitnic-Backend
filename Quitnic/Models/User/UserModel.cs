using System.ComponentModel.DataAnnotations;

namespace Quitnic.Models.User
{
    public class UserModel
    {
        public required Guid Id { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
