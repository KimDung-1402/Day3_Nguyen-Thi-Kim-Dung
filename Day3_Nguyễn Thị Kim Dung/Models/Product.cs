using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day3_Nguyễn_Thị_Kim_Dung.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; } 
        
        
        public string Name { get; set; }
        public double Price { get; set; } // Bạn có thể dùng decimal thay cho double
        public string Description { get; set; }

        [ForeignKey("Category")]

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}