using Hackathon.Db.Models;

namespace HackathonCallCenter.Models
{
    public class CallViewModel
    {
        public Agent? Agent { get; set; }

        public string AudioFileUrl { get; set; } = null!;
        public List<DialogueLine>? TranscriptionText { get; set; }

        public string? PersonalData { get; set; }

        // CallAnalysis
        public string GoalAchievement { get; set; } = ""; // Достижение цели звонка
        public bool HasConflict { get; set; } = false; // Наличие конфликта
        public string OperatorQuality { get; set; } = ""; // Качество работы оператора
        //public List<string> KeyWords { get; set; } = new(); // Выявленные ключевые слова
        public string ScriptCompliance { get; set; } = ""; // Соблюдение скрипта диалога

        // OperatorRecommendations
        public string ResponseSpeed { get; set; } = ""; // Скорость ответа
        public string SpeechGrammar { get; set; } = ""; // Грамотность речи
        public string ActiveListening { get; set; } = ""; // Активное слушание
        public string ProblemSolving { get; set; } = ""; // Решение проблемы
    }
}
