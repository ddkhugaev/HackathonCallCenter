using Microsoft.AspNetCore.Mvc;

namespace HackathonCallCenter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Главная";
            return View();
        }

        public IActionResult Calls()
        {
            ViewData["Title"] = "Звонки";
            return View();
        }

        public IActionResult Operators()
        {
            ViewData["Title"] = "Операторы";
            return View();
        }

        public IActionResult Analytics()
        {
            ViewData["Title"] = "Аналитика";
            return View();
        }

        public IActionResult AiAnalysis()
        {
            ViewData["Title"] = "Анализ эмоций";
            return View();
        }

        public IActionResult Recommendations()
        {
            ViewData["Title"] = "Рекомендации";
            return View();
        }

        public IActionResult ProblemDetection()
        {
            ViewData["Title"] = "Выявление проблем";
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}
    }
}