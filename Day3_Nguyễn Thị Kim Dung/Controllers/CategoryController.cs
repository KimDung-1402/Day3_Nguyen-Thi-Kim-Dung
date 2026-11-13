using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Day3_Nguyễn_Thị_Kim_Dung.Data;
using Day3_Nguyễn_Thị_Kim_Dung.Models;

namespace Day3_Nguyễn_Thị_Kim_Dung.Controllers
{

    public class CategoryController : Controller
    {

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }
        // GET: CategoryController
        public IActionResult Index(string searchString)
        {
            var categories = SeedClass.categories.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(c =>
                    c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    (!string.IsNullOrEmpty(c.Description) && c.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                );
            }

            ViewBag.CurrentSearch = searchString;
            return View(categories.ToList());
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            _logger.LogInformation("detail action called: {Id}", id);
            _logger.LogError("This is a test: {Id}", id);
            var category = SeedClass.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Models.Category category)
        {
            if (ModelState.IsValid)
            {
                int maxId = SeedClass.categories.Max(c => c.Id);

                category.Id = maxId + 1;

                SeedClass.categories.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit([FromRoute] int id)
        {
            Category category = SeedClass.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] Category category)
        {
            var exitstiongCategory = SeedClass.categories.FirstOrDefault(c => c.Id == category.Id);
            if (exitstiongCategory == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                exitstiongCategory.Name = category.Name;
                exitstiongCategory.Description = category.Description;
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = SeedClass.categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([FromRoute] int id)
        {
            try
            {
                var categorie = SeedClass.categories.FirstOrDefault(c => c.Id == id);
                if (categorie != null)
                {
                    SeedClass.categories.Remove(categorie);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
