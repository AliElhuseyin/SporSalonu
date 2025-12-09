using System.ComponentModel.DataAnnotations;

namespace FitnessCenterSystem.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ServiceType { get; set; } // Fitness, Yoga, Pilates

        [Required]
        public int Duration { get; set; } // Dakika

        [Required]
        public decimal Price { get; set; }

        public string? Notes { get; set; }

        [Required]
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled

        // Foreign Keys
        public int MemberId { get; set; }
        public int TrainerId { get; set; }
        public int GymId { get; set; }

        // Navigation Properties
        public virtual Member Member { get; set; }
        public virtual Trainer Trainer { get; set; }
        public virtual Gym Gym { get; set; }
    }
}