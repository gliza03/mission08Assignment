using System.ComponentModel.DataAnnotations;

namespace mission8Assignment.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
