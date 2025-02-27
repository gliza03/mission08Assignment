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
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // track task completion
        public bool Completed { get; set; } = false;
    }
}
