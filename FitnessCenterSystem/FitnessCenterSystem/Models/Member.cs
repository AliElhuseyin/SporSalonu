using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessCenterSystem.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        // Vücut bilgileri (AI için)
        public double? Height { get; set; } // cm
        public double? Weight { get; set; } // kg
        public string? BodyType { get; set; }

        // İlişkiler
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
