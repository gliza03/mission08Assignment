using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mission8Assignment.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public string TaskName { get; set; }

        public string? DueDate { get; set; }

        [Required]
        public string Quadrant { get; set; }

        // Foreign Key
        [Required]
        public int CategoryId { get; set; }

        // Removed the Category navigation property as per your request
        public Category Category { get; set; }
        // Track task completion
        public bool Completed { get; set; } = false;
    }

}
