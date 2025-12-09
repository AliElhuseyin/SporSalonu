using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessCenterSystem.Models
{
    public class Trainer
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

        [Required]
        public string Specialization { get; set; } // Kas geliştirme, Yoga, vb.

        public string Bio { get; set; }

        // Çalışma saatleri
        public string AvailableHours { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

        // İlişkiler
        public int GymId { get; set; }
        public virtual Gym Gym { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}