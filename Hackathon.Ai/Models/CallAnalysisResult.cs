using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Ai.Models
{
    public class CallAnalysisResult
    {
        // 1. Транскрипция диалога
        public string Transcription { get; set; } = null!;

        // 2. AI анализ звонка
        public CallAnalysis Analysis { get; set; } = new();

        // 3. Рекомендации для оператора
        public OperatorRecommendations Recommendations { get; set; } = new();
    }
}
