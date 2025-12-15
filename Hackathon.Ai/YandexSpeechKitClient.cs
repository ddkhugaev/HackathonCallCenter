namespace Hackathon.Ai
{
    using DotNetEnv;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;

    public class YandexSpeechKitClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public YandexSpeechKitClient()
        {
            Env.Load();

            _apiKey = Env.GetString("YANDEX_API_KEY", "Variable not found");
            _httpClient = new HttpClient();

            // Настраиваем заголовок Authorization с API Key
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Api-Key", _apiKey);
        }

        public async Task<string> RecognizeAudioAsync(string audioFileUri, string languageCode = "ru-RU")
        {
            try
            {
                Console.WriteLine("Начало анализа аудио записи...");

                // 1. Запускаем операцию распознавания
                var operationId = await StartRecognitionAsync(audioFileUri, languageCode);
                Console.WriteLine($"Operation started. ID: {operationId}");

                // 2. Ожидаем завершения
                var operationResult = await WaitForOperationCompletionAsync(operationId);

                // 3. Извлекаем текст
                return ExtractTranscribedText(operationResult);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Error: {ex.StatusCode} - {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        private async Task<string> StartRecognitionAsync(string audioFileUri, string languageCode)
        {
            var request = new
            {
                config = new
                {
                    specification = new
                    {
                        languageCode = languageCode
                        // Дополнительные параметры при необходимости:
                        // model = "general",
                        // profanityFilter = false,
                        // audioEncoding = "OGG_OPUS"
                    }
                },
                audio = new
                {
                    uri = audioFileUri
                }
            };

            string startUrl = "https://transcribe.api.cloud.yandex.net/speech/stt/v2/longRunningRecognize";
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var startResponse = await _httpClient.PostAsync(startUrl, content);

            // Добавляем отладку для 401 ошибки
            if (!startResponse.IsSuccessStatusCode)
            {
                var errorContent = await startResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"API Error Response: {errorContent}");
            }

            startResponse.EnsureSuccessStatusCode();

            var startResponseContent = await startResponse.Content.ReadAsStringAsync();
            var startJsonDocument = JsonDocument.Parse(startResponseContent);

            return startJsonDocument.RootElement
                .GetProperty("id")
                .GetString() ?? throw new InvalidOperationException("Operation ID not received");
        }

        private async Task<JsonDocument> WaitForOperationCompletionAsync(string operationId)
        {
            string statusUrl = $"https://operation.api.cloud.yandex.net/operations/{operationId}";

            int attempt = 0;
            while (true)
            {
                attempt++;
                await Task.Delay(1000); // Ждем 1 секунду

                var statusResponse = await _httpClient.GetAsync(statusUrl);
                statusResponse.EnsureSuccessStatusCode();

                var statusContent = await statusResponse.Content.ReadAsStringAsync();
                var statusDocument = JsonDocument.Parse(statusContent);

                if (statusDocument.RootElement.GetProperty("done").GetBoolean())
                {
                    Console.WriteLine($"Recognition completed (attempt {attempt})");
                    return statusDocument;
                }

                Console.WriteLine($"Not ready... (attempt {attempt})");
            }
        }

        private string ExtractTranscribedText(JsonDocument operationResult)
        {
            // Проверяем наличие ошибки
            if (operationResult.RootElement.TryGetProperty("error", out var errorElement))
            {
                var errorCode = errorElement.GetProperty("code").GetInt32();
                var errorMessage = errorElement.GetProperty("message").GetString();
                throw new Exception($"Recognition error {errorCode}: {errorMessage}");
            }

            var responseElement = operationResult.RootElement.GetProperty("response");
            var chunks = responseElement.GetProperty("chunks").EnumerateArray();

            var textBuilder = new StringBuilder();

            Console.WriteLine("\nText chunks:");
            foreach (var chunk in chunks)
            {
                var text = chunk
                    .GetProperty("alternatives")[0]
                    .GetProperty("text")
                    .GetString();

                Console.WriteLine($"  - {text}");
                textBuilder.AppendLine(text);
            }

            return textBuilder.ToString().Trim();
        }
    }
}
