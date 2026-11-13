using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Day3_Nguyễn_Thị_Kim_Dung.Models;
using Day3_Nguyễn_Thị_Kim_Dung.Data;

namespace Day3_Nguyễn_Thị_Kim_Dung.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(string searchString, int? categoryId, string sortField, string sortDir, int pageNumber = 1)
        {
            var products = SeedClass.products.AsQueryable();

            // 1. TÌM KIẾM VÀ LỌC
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p =>
                    p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    (!string.IsNullOrEmpty(p.Description) && p.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                );
            }
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            // 2. SẮP XẾP (Tách biệt Trường dữ liệu và Chiều sắp xếp)
            if (sortField == "Price")
            {
                products = (sortDir == "desc") ? products.OrderByDescending(p => p.Price) : products.OrderBy(p => p.Price);
            }
            else // Mặc định là sắp xếp theo Name
            {
                products = (sortDir == "desc") ? products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name);
            }

            // 3. PHÂN TRANG
            int pageSize = 2; // Số item trên 1 trang
            int totalItems = products.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            if (pageNumber < 1) pageNumber = 1;
            if (pageNumber > totalPages && totalPages > 0) pageNumber = totalPages;

            products = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            // 4. TRUYỀN DATA SANG VIEW
            ViewBag.Categories = new SelectList(SeedClass.categories, "Id", "Name", categoryId);
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentCategory = categoryId;
            ViewBag.SortField = string.IsNullOrEmpty(sortField) ? "Name" : sortField;
            ViewBag.SortDir = string.IsNullOrEmpty(sortDir) ? "asc" : sortDir;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(products.ToList());
        }

        public IActionResult Details(int id)
        {
            var product = SeedClass.products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(SeedClass.categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = SeedClass.products.Any() ? SeedClass.products.Max(p => p.Id) + 1 : 1;
                SeedClass.products.Add(product);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(SeedClass.categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = SeedClass.products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            ViewBag.CategoryId = new SelectList(SeedClass.categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = SeedClass.products.FirstOrDefault(p => p.Id == product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                    existingProduct.Description = product.Description;
                    existingProduct.CategoryId = product.CategoryId;
                }
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(SeedClass.categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = SeedClass.products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = SeedClass.products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                SeedClass.products.Remove(product);
            }
            return RedirectToAction("Index");
        }
    }
}