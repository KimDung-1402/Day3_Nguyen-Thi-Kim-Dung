using Day3_Nguyễn_Thị_Kim_Dung.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Day3_Nguyễn_Thị_Kim_Dung.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
