using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessCenterSystem.Models
{
    public class Gym
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string WorkingHours { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        // İlişkiler
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}