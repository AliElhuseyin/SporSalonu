using System.ComponentModel.DataAnnotations;

namespace FitnessCenterSystem.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Duration { get; set; } // Dakika

        [Required]
        public decimal Price { get; set; }

        // Foreign Key
        public int GymId { get; set; }

        // Navigation Property
        public virtual Gym Gym { get; set; }
    }
}
