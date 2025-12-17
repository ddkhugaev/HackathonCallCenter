using System.Diagnostics;
using HackathonCallCenter.Models;
using Microsoft.AspNetCore.Mvc;
using HackathonCallCenter.Services;
using Hackathon.Db;
using Hackathon.Db.Models;

namespace HackathonCallCenter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Analyzer analyzer;
        private readonly ICallsRepository callsRepository;
        private readonly IAgentsRepository agentsRepository;

        public HomeController(ILogger<HomeController> logger, Analyzer analyzer, ICallsRepository callsRepository, IAgentsRepository agentsRepository)
        {
            _logger = logger;
            this.analyzer = analyzer;
            this.callsRepository = callsRepository;
            this.agentsRepository = agentsRepository;
        }

        public async Task<string> Test()
        {
            string ans = await analyzer.TryDecodeAudioAsync("https://storage.yandexcloud.net/pictures-sogu/%D0%98%D0%BD%D1%82%D0%B5%D1%80%D0%BD%D0%B5%D1%82%20%D0%BA%D0%BE%D0%BB%D0%BB%20%D1%86%D0%B5%D0%BD%D1%82%D1%80.ogg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=YCAJEpmZKNWc7mXWszkSkTE2E%2F20251216%2Fru-central1%2Fs3%2Faws4_request&X-Amz-Date=20251216T155130Z&X-Amz-Expires=3600&X-Amz-Signature=592c874bbb45087b6c107a0f97bb76dfe61f50f44a88b4ec9f144181a693d378&X-Amz-SignedHeaders=host&response-content-disposition=attachment");
            ans += await analyzer.TrySplitByRoleAsync(ans);
            return ans;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

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

        [HttpPost]
        public async Task<IActionResult> AddCall(string url, string fullName)
        {
            var agent = await agentsRepository.TryGetByFullNameAsync(fullName);
            if (agent == null)
            {
                agent = new Agent()
                {
                    FullName = fullName
                };
                await agentsRepository.AddAsync(agent);
            }
            //string url = "https://storage.yandexcloud.net/pictures-sogu/%D0%98%D0%BD%D1%82%D0%B5%D1%80%D0%BD%D0%B5%D1%82%20%D0%BA%D0%BE%D0%BB%D0%BB%20%D1%86%D0%B5%D0%BD%D1%82%D1%80.ogg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=YCAJEpmZKNWc7mXWszkSkTE2E%2F20251216%2Fru-central1%2Fs3%2Faws4_request&X-Amz-Date=20251216T155130Z&X-Amz-Expires=3600&X-Amz-Signature=592c874bbb45087b6c107a0f97bb76dfe61f50f44a88b4ec9f144181a693d378&X-Amz-SignedHeaders=host&response-content-disposition=attachment";
            var call = await analyzer.Analyze(url, agent);
            await agentsRepository.AddAsync(agent);
            await callsRepository.AddAsync(call);
            return RedirectToAction("CallAnalyze", new {id = call.Id});
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData["Title"] = "Анализ разговора";

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
