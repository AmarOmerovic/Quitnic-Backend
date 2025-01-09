using System.ComponentModel.DataAnnotations;

namespace Quitnic.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
