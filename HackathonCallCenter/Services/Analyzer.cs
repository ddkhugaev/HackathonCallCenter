using Hackathon.Ai;
using Hackathon.Ai.Models;
using Hackathon.Db.Models;
namespace HackathonCallCenter.Services
{
    public class Analyzer
    {
        private readonly YandexSpeechKitClient speechClient;
        private readonly YandexGptClient gptClient;

        public Analyzer()
        {
            speechClient = new YandexSpeechKitClient();
            gptClient = new YandexGptClient();
        }
        public async Task<Call> Analyze(string url, Agent agent)
        {
            // чистая расшифровка
            string rawText = await speechClient.RecognizeAudioAsync(url);
            // разделение расшифровки на роли
            string dialogue = await SplitByRoleAsync(rawText);
            // выделение персональных данных
            string personalData = await GetPersonalData(dialogue);
            // анализ диалога
            string scriptDescription = """
                Стандартный скрипт колл-центра включает:
                1. Приветствие и представление
                2. Выяснение проблемы клиента
                3. Предложение решения
                4. Подтверждение удовлетворенности
                5. Прощание
                """;
            var result = await gptClient.AnalyzeCallDialogueAsync(dialogue, scriptDescription);
            return ConvertCallAnalysisResultToCall(result, url, agent, personalData);
        }
        public async Task<string> SplitByRoleAsync(string text)
        {
            string prompt = "Определение дикторов в результатах распознавания. Говорит 'оператор' и 'клиент'. Ты должен перед каждым началом фразы приписать кто ее говорит.";
            return await gptClient.GetGptResponseAsync(text, prompt);
        }
        public async Task<string> GetPersonalData(string dialogue)
        {
            string prompt = "Выдели (если есть) продиктованные клиентом персональные данные (адрес, номер счета и т.п.), не искажай смысл.";
            return await gptClient.GetGptResponseAsync(dialogue, prompt);
        }
        public Call ConvertCallAnalysisResultToCall(CallAnalysisResult callAnalysisResult, string url, Agent agent, string personalData = "(не найдено)")
        {
            Call call = new Call()
            {
                AgentId = agent.Id,
                Agent = agent,
                AudioFileUrl = url,
                TranscriptionText = callAnalysisResult.Transcription,
                PersonalData = personalData,
                GoalAchievement = callAnalysisResult.Analysis.GoalAchievement,
                HasConflict = callAnalysisResult.Analysis.HasConflict,
                OperatorQuality = callAnalysisResult.Analysis.OperatorQuality,
                //KeyWords = callAnalysisResult.Analysis.KeyWords,
                ScriptCompliance = callAnalysisResult.Analysis.ScriptCompliance,
                ResponseSpeed = callAnalysisResult.Recommendations.ResponseSpeed,
                SpeechGrammar = callAnalysisResult.Recommendations.SpeechGrammar,
                ActiveListening = callAnalysisResult.Recommendations.ActiveListening,
                ProblemSolving = callAnalysisResult.Recommendations.ProblemSolving
            };
            return call;
        }
    }
}
