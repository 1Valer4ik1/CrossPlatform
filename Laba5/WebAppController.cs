using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Створіть представлення Index.cshtml
        }

        public IActionResult About()
        {
            return View(); // Створіть представлення About.cshtml з описом роботи
        }
    }
}
