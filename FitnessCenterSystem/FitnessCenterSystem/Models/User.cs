using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FitnessCenterSystem.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Required]
        public string Role { get; set; } = "Member"; // Admin, Member
    }
}
