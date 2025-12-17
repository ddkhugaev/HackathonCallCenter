using Hackathon.Ai;
using HackathonCallCenter;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var speechClient = new YandexSpeechKitClient();

            //Тестовая ссылка на аудио (можно заменить на свою)
            string audioUri = "https://storage.yandexcloud.net/pictures-sogu/%D0%9C%D1%83%D0%B7%D0%B5%D0%B9%20%D0%BA%D0%BE%D0%BB%D0%BB%20%D1%86%D0%B5%D0%BD%D1%82%D1%80.ogg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=YCAJEpmZKNWc7mXWszkSkTE2E%2F20251217%2Fru-central1%2Fs3%2Faws4_request&X-Amz-Date=20251217T085443Z&X-Amz-Expires=43200&X-Amz-Signature=5397e5fe70ab2929fda93b576dba39bfe86db15c6d2e7c970345a7ae36a267d9&X-Amz-SignedHeaders=host&response-content-disposition=attachment";

            Console.WriteLine($"\nИспользуемая ссылка на аудио: {audioUri}");

            // Запускаем распознавание
            Console.WriteLine("\nЗапускаю распознавание...");
            var recognizedText = await speechClient.RecognizeAudioAsync(audioUri);

            // Выводим результат
            //Console.WriteLine("\n" + new string('=', 50));
            //Console.WriteLine("РАСПОЗНАННЫЙ ТЕКСТ:");
            //Console.WriteLine(new string('=', 50));
            //Console.WriteLine(recognizedText);
            //Console.WriteLine(new string('=', 50));

            Console.WriteLine("\n===Обозначаем роли в диалоге===");
            var gptClient = new YandexGptClient();

            var transcript = await gptClient.GetGptResponseAsync(recognizedText, "Определение дикторов в результатах распознавания. Говорит 'оператор' и 'клиент'. Ты должен перед каждым началом фразы приписать кто ее говорит.");

            var scriptDescription = """
        Стандартный скрипт колл-центра включает:
        1. Приветствие и представление
        2. Выяснение проблемы клиента
        3. Предложение решения
        4. Подтверждение удовлетворенности
        5. Прощание
        """;

            Console.WriteLine("\n\n");

            // Получаем все нужные данные:
            var analysisResult = await gptClient.AnalyzeCallDialogueAsync(transcript, scriptDescription);


            Console.WriteLine($"Соблюдение скрипта: {analysisResult.Analysis.ScriptCompliance}");

            Console.WriteLine("Личные данные клиента:");
            var personalData = await gptClient.GetGptResponseAsync(transcript, "Выдели (если есть) продиктованные клиентом персональные данные (адрес, номер счета и т.п.), не искажай смысл.");
            Console.WriteLine(personalData);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            Console.WriteLine("\n❌ ОШИБКА 401: Неверная авторизация");
            Console.WriteLine("\nПолная ошибка: " + ex.Message);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"\n❌ ОШИБКА HTTP {ex.StatusCode}: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ ОШИБКА: {ex.GetType().Name}: {ex.Message}");
            Console.WriteLine("StackTrace: " + ex.StackTrace);
        }



        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}
