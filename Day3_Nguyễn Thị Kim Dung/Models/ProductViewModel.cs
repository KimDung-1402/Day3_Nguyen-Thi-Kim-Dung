using Microsoft.AspNetCore.Mvc;

namespace Day3_Nguyễn_Thị_Kim_Dung.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public String Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public String Description{ get; set; }

        public int CategoryId { get; set; }

    }
}
