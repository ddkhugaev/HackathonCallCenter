using System.Diagnostics;
using HackathonCallCenter.Models;
using Microsoft.AspNetCore.Mvc;
using HackathonCallCenter.Services;
using Hackathon.Db;

namespace HackathonCallCenter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Analyzer analyzer;
        private readonly ICallsRepository callsRepository;

        public HomeController(ILogger<HomeController> logger, Analyzer analyzer, ICallsRepository callsRepository)
        {
            _logger = logger;
            this.analyzer = analyzer;
            this.callsRepository = callsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<string> Test()
        {
            var result = await analyzer.Analyze("https://storage.yandexcloud.net/pictures-sogu/%D0%98%D0%BD%D1%82%D0%B5%D1%80%D0%BD%D0%B5%D1%82%20%D0%BA%D0%BE%D0%BB%D0%BB%20%D1%86%D0%B5%D0%BD%D1%82%D1%80.ogg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=YCAJEpmZKNWc7mXWszkSkTE2E%2F20251216%2Fru-central1%2Fs3%2Faws4_request&X-Amz-Date=20251216T155130Z&X-Amz-Expires=3600&X-Amz-Signature=592c874bbb45087b6c107a0f97bb76dfe61f50f44a88b4ec9f144181a693d378&X-Amz-SignedHeaders=host&response-content-disposition=attachment");
            string ans = $"{result.Analysis.GoalAchievement}\n{result.Analysis.HasConflict}\n{result.Analysis.OperatorQuality}\n{result.Analysis.KeyWords}\n{result.Analysis.ScriptCompliance}";
            return ans;
        }
        public async Task<string> GetAll()
        {
            //string ans = await analyzer.TryDecodeAudioAsync("https://storage.yandexcloud.net/pictures-sogu/%D0%98%D0%BD%D1%82%D0%B5%D1%80%D0%BD%D0%B5%D1%82%20%D0%BA%D0%BE%D0%BB%D0%BB%20%D1%86%D0%B5%D0%BD%D1%82%D1%80.ogg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=YCAJEpmZKNWc7mXWszkSkTE2E%2F20251216%2Fru-central1%2Fs3%2Faws4_request&X-Amz-Date=20251216T155130Z&X-Amz-Expires=3600&X-Amz-Signature=592c874bbb45087b6c107a0f97bb76dfe61f50f44a88b4ec9f144181a693d378&X-Amz-SignedHeaders=host&response-content-disposition=attachment");
            //ans += await analyzer.TrySplitByRoleAsync(ans);
            var list = await callsRepository.GetAllAsync();
            return list.ToString();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
