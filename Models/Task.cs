using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mission8Assignment.Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int TaskId { get; set; }

        [Required]
        public string TaskName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        public string Quadrant { get; set; }

        // Foreign Key
        
        [Required]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        // Removed the Category navigation property as per your request
        public Category? Category { get; set; }
        // Track task completion
        public bool Completed { get; set; } = false;
    }

}
