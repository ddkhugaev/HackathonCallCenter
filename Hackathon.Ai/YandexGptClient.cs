using DotNetEnv;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackathon.Ai
{
    using Hackathon.Ai.Models;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class YandexGptClient
    {
        string _apiKey;
        readonly string _folderId;
        readonly HttpClient _httpClient;

        public YandexGptClient()
        {
            _folderId = Constants._idYandexFolder;
            _httpClient = new HttpClient();
            Env.TraversePath().Load();
            _apiKey = Env.GetString("YANDEX_GPT_API_KEY", "Variable not found");
        }

        public async Task<string> GetGptResponseAsync(string userMessage, string promt)
        {
            var request = new
            {
                modelUri = $"gpt://{_folderId}/yandexgpt",
                completionOptions = new
                {
                    stream = false,
                    temperature = 0.6,
                    maxTokens = "2000"
                },
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        text = $"{promt}\n\nОтвечай простым текстом без форматирования (без звездочек, жирного текста, курсива и т.д.)"
                    },
                    new
                    {
                        role = "user",
                        text = userMessage
                    }
                }
            };

            string url = "https://llm.api.cloud.yandex.net/foundationModels/v1/completion";
            var json = JsonSerializer.Serialize(request);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Api-Key {_apiKey}");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(responseContent);

            return jsonDocument.RootElement
                .GetProperty("result")
                .GetProperty("alternatives")[0]
                .GetProperty("message")
                .GetProperty("text")
                .GetString()!;
        }

        

        // Аналогичный метод, но возвращающий JSON напрямую
        public async Task<string> GetGptJsonResponseAsync(string userMessage, string promt)
        {
            // Добавляем указание на JSON формат в промпт
            string jsonPrompt = $"{promt}\n\nВерни ответ ТОЛЬКО в формате JSON, без дополнительных текстов или форматирования.";

            var request = new
            {
                modelUri = $"gpt://{_folderId}/yandexgpt",
                completionOptions = new
                {
                    stream = false,
                    temperature = 0.3, // Низкая температура для более стабильных ответов
                    maxTokens = "2000"
                },
                messages = new[]
                {
            new
            {
                role = "system",
                text = jsonPrompt
            },
            new
            {
                role = "user",
                text = userMessage
            }
        }
            };

            string url = "https://llm.api.cloud.yandex.net/foundationModels/v1/completion";
            var json = JsonSerializer.Serialize(request);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Api-Key {_apiKey}");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(responseContent);

            string rawResponse = jsonDocument.RootElement
                .GetProperty("result")
                .GetProperty("alternatives")[0]
                .GetProperty("message")
                .GetProperty("text")
                .GetString()!;

            // Очищаем и возвращаем JSON
            return CleanJsonResponse(rawResponse);
        }

        public async Task<CallAnalysisResult> AnalyzeCallDialogueAsync(string formattedDialogue, string? scriptDescription = null)
        {
            string prompt = """
            Ты - аналитик колл-центра. Проанализируй диалог между оператором и клиентом.

            Требуется вернуть ответ в строгом JSON формате со следующей структурой:
            {
                "Analysis": {
                    "GoalAchievement": "оценка_достижения_цели",
                    "HasConflict": true/false,
                    "OperatorQuality": "оценка_качества",
                    "KeyWords": ["слово1", "слово2", "слово3"],
                    "ScriptCompliance": "оценка_соблюдения_скрипта"
                },
                "Recommendations": {
                    "ResponseSpeed": "оценка_скорости",
                    "SpeechGrammar": "оценка_грамматики",
                    "ActiveListening": "оценка_слушания",
                    "ProblemSolving": "оценка_решения"
                }
            }

            Инструкции по заполнению:
            1. GoalAchievement: "полное", "частичное", "недостигнуто" или другая краткая характеристика
            2. HasConflict: true если был конфликт, false если нет
            3. OperatorQuality: "отличное", "хорошее", "удовлетворительное", "неудовлетворительное"
            4. KeyWords: 3-5 ключевых слов из диалога, характеризующих суть обращения
            5. ScriptCompliance: "полное", "частичное", "несоблюдение" - оценка соблюдения оператором скрипта диалога

            Для рекомендаций используй ТОЛЬКО одно характеризующее слово:
            - ResponseSpeed: "быстрая", "средняя", "медленная"
            - SpeechGrammar: "грамотная", "приемлемая", "неграмотная"
            - ActiveListening: "активное", "пассивное", "отсутствует"
            - ProblemSolving: "эффективное", "среднее", "неэффективное"
            """;

            if (!string.IsNullOrEmpty(scriptDescription))
            {
                prompt += $"\nСкрипт диалога, который должен соблюдать оператор:\n{scriptDescription}\n";
            }
            else
            {
                prompt += "\nОцени соблюдение стандартного скрипта диалога для колл-центра.\n";
            }

            prompt += "\nДиалог:";

            try
            {
                // Используем метод, который возвращает очищенный JSON
                string jsonResponse = await GetGptJsonResponseAsync(formattedDialogue, prompt);

                Console.WriteLine($"\n📥 Полученный JSON: {jsonResponse}");

                // Парсим JSON
                var jsonDocument = JsonDocument.Parse(jsonResponse);
                var root = jsonDocument.RootElement;

                var result = new CallAnalysisResult
                {
                    Transcription = formattedDialogue
                };

                // Заполняем анализ
                var analysisElement = root.GetProperty("Analysis");
                result.Analysis = new CallAnalysis
                {
                    GoalAchievement = analysisElement.GetProperty("GoalAchievement").GetString() ?? "",
                    HasConflict = analysisElement.GetProperty("HasConflict").GetBoolean(),
                    OperatorQuality = analysisElement.GetProperty("OperatorQuality").GetString() ?? "",
                    ScriptCompliance = analysisElement.GetProperty("ScriptCompliance").GetString() ?? "",
                    KeyWords = analysisElement.GetProperty("KeyWords").EnumerateArray()
                                             .Select(x => x.GetString() ?? "")
                                             .Where(x => !string.IsNullOrEmpty(x))
                                             .ToList()
                };

                // Заполняем рекомендации
                var recommendationsElement = root.GetProperty("Recommendations");
                result.Recommendations = new OperatorRecommendations
                {
                    ResponseSpeed = recommendationsElement.GetProperty("ResponseSpeed").GetString() ?? "",
                    SpeechGrammar = recommendationsElement.GetProperty("SpeechGrammar").GetString() ?? "",
                    ActiveListening = recommendationsElement.GetProperty("ActiveListening").GetString() ?? "",
                    ProblemSolving = recommendationsElement.GetProperty("ProblemSolving").GetString() ?? ""
                };

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Ошибка: {ex.Message}");
                return CreateFallbackResult(formattedDialogue);
            }
        }

        private string CleanJsonResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
                return response;

            string cleaned = response.Trim();

            // Убираем обратные кавычки и метки формата
            if (cleaned.StartsWith("```json"))
            {
                cleaned = cleaned.Substring(7);
            }
            else if (cleaned.StartsWith("```"))
            {
                cleaned = cleaned.Substring(3);
            }

            if (cleaned.EndsWith("```"))
            {
                cleaned = cleaned.Substring(0, cleaned.Length - 3);
            }

            // Убираем лишние пробелы и переносы строк
            cleaned = cleaned.Trim();

            // Ищем начало JSON (иногда GPT добавляет пояснения перед JSON)
            int jsonStart = cleaned.IndexOf('{');
            if (jsonStart > 0)
            {
                cleaned = cleaned.Substring(jsonStart);
            }

            // Ищем конец JSON
            int jsonEnd = cleaned.LastIndexOf('}');
            if (jsonEnd > 0 && jsonEnd < cleaned.Length - 1)
            {
                cleaned = cleaned.Substring(0, jsonEnd + 1);
            }

            return cleaned;
        }

        private CallAnalysisResult CreateFallbackResult(string dialogueLines)
        {
            return new CallAnalysisResult
            {
                Transcription = dialogueLines,
                Analysis = new CallAnalysis
                {
                    GoalAchievement = "неопределено",
                    HasConflict = false,
                    OperatorQuality = "неопределено",
                    ScriptCompliance = "неопределено",
                    KeyWords = new List<string>()
                },
                Recommendations = new OperatorRecommendations
                {
                    ResponseSpeed = "средняя",
                    SpeechGrammar = "приемлемая",
                    ActiveListening = "пассивное",
                    ProblemSolving = "среднее"
                }
            };
        }
    }
}
