namespace Hackathon.Ai.Models
{
    public class CallAnalysis
    {
        public string GoalAchievement { get; set; } = ""; // Достижение цели звонка
        public bool HasConflict { get; set; } = false; // Наличие конфликта
        public string OperatorQuality { get; set; } = ""; // Качество работы оператора
        public List<string> KeyWords { get; set; } = new(); // Выявленные ключевые слова
        public string ScriptCompliance { get; set; } = ""; // Соблюдение скрипта диалога
    }
}
