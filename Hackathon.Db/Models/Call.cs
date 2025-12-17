using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Db.Models
{
    public class Call
    {
        public int Id { get; set; }

        public int AgentId { get; set; }
        public Agent? Agent { get; set; }

        public string AudioFileUrl { get; set; } = null!;
        public string? TranscriptionText { get; set; }

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
