using Hackathon.Ai;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var speechClient = new YandexSpeechKitClient();

            //Тестовая ссылка на аудио (можно заменить на свою)
            string audioUri = "https://storage.yandexcloud.net/pictures-sogu/%D0%95%D0%BB%D0%B5%D0%BD%D0%B0%20%D0%BA%D0%BE%D0%BB%D0%BB%20%D1%86%D0%B5%D0%BD%D1%82%D1%80%20%D0%BC%D1%83%D0%B7%D0%B5%D0%B9.ogg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=YCAJEpmZKNWc7mXWszkSkTE2E%2F20251216%2Fru-central1%2Fs3%2Faws4_request&X-Amz-Date=20251216T142419Z&X-Amz-Expires=36000&X-Amz-Signature=194afa1940feb6a454947635c1c3ddbfb9cba3f1183ec2c3167edf51fa928b90&X-Amz-SignedHeaders=host&response-content-disposition=attachment";

            Console.WriteLine($"\nИспользуемая ссылка на аудио: {audioUri}");

            // Запускаем распознавание
            Console.WriteLine("\nЗапускаю распознавание...");
            var recognizedText = await speechClient.RecognizeAudioAsync(audioUri);

            // Выводим результат
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("РАСПОЗНАННЫЙ ТЕКСТ:");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine(recognizedText);
            Console.WriteLine(new string('=', 50));

            Console.WriteLine("\n===Обозначаем роли в диалоге===");
            var gptClient = new YandexGptClient();
            var result = await gptClient.GetGptResponseAsync(recognizedText, "Определение дикторов в результатах распознавания. Говорит 'оператор' и 'клиент'. Ты должен перед каждым началом фразы приписать кто ее говорит.");
            Console.WriteLine(result);

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
