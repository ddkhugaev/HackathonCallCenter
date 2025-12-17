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
        public IActionResult CallAnalysis()
        {
            ViewData["Title"] = "Анализ разговора";

            // Можно передать данные через ViewBag или ViewData
            ViewData["CallId"] = "#2457";
            ViewData["PhoneNumber"] = "+7 927 368 99 93";
            ViewData["OperatorName"] = "Анна Иванова";
            ViewData["CallDate"] = "Сегодня, 10:24";
            ViewData["Duration"] = "4:18";
            ViewData["Status"] = "Успешный";

            return View();
        }
    }
}