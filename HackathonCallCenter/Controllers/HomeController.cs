using System.Diagnostics;
using HackathonCallCenter.Models;
using Microsoft.AspNetCore.Mvc;
using HackathonCallCenter.Services;
using Hackathon.Db;
using Hackathon.Db.Models;
using System.Threading.Tasks;

namespace HackathonCallCenter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Analyzer analyzer;
        private readonly ICallsRepository callsRepository;
        private readonly IAgentsRepository agentsRepository;

        public HomeController(ILogger<HomeController> logger, Analyzer analyzer,
                              ICallsRepository callsRepository, IAgentsRepository agentsRepository)
        {
            _logger = logger;
            this.analyzer = analyzer;
            this.callsRepository = callsRepository;
            this.agentsRepository = agentsRepository;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Главная";
            return View();
        }

        // Звонки
        public async Task<IActionResult> Calls()
        {
            ViewData["Title"] = "Звонки";
            var calls = await callsRepository.GetAllAsync();
            return View(calls);
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

        // Анализ эмоций для конкретного звонка
        public async Task<IActionResult> AiAnalysis(int id)
        {
            ViewData["Title"] = "Анализ эмоций";
            var call = await callsRepository.TryGetByIdAsync(id);
            return View(call);
        }

        public IActionResult Recommendations()
        {
            ViewData["Title"] = "Рекомендации";
            return View();
        }

        // ВЫЯВЛЕНИЕ ПРОБЛЕМ - добавить этот метод
        public IActionResult ProblemDetection()
        {
            ViewData["Title"] = "Выявление проблем";
            return View();
        }

        // АНАЛИЗ РАЗГОВОРА (CallAnalysis) - добавить этот метод
        public IActionResult CallAnalysis()
        {
            ViewData["Title"] = "Анализ разговора";
            return View();
        }

        // Добавить звонок
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

            var call = await analyzer.Analyze(url, agent);
            await callsRepository.AddAsync(call);
            return RedirectToAction("AiAnalysis", new { id = call.Id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}