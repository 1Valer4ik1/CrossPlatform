using Microsoft.AspNetCore.Mvc;
using LabTools;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class LabController : Controller
    {
        public IActionResult Lab1()
        {
            return View(); // Створіть представлення Lab1.cshtml з полями вводу та виводу
        }

        public IActionResult Lab2()
        {
            return View(); // Створіть представлення Lab2.cshtml
        }

        public IActionResult Lab3()
        {
            return View(); // Створіть представлення Lab3.cshtml
        }

        [HttpPost]
        public IActionResult RunLab1(string input, string output)
        {
            SheetProcessor.ExecuteLab1(input, output);
            return View(); // Повертає на Lab1 з результатами
        }
    }
}
