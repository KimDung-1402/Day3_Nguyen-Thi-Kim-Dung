using Day3_Nguyễn_Thị_Kim_Dung.Models;
namespace Day3_Nguyễn_Thị_Kim_Dung.Data
{
    public class SeedClass
    {
        public static List<Category> categories = new List<Category>
        {
            new Category() { Id = 1, Name = "Category 1", Description = "Description for Category 1" },
            new Category() { Id = 2, Name = "Category 2", Description = "Description for Category 2" },
            new Category() { Id = 3, Name = "Category 3", Description = "Description for Category 3" }
        };

        public static List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Sản phẩm A", Price = 150000, CategoryId = 1, Description = "Mô tả A" },
            new Product { Id = 2, Name = "Sản phẩm B", Price = 300000, CategoryId = 2, Description = "Mô tả B" }
        };
    }
}
