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
            string audioUri = "https://storage.yandexcloud.net/pictures-sogu/%D1%82%D0%B5%D1%81%D1%82%20%D0%B3%D1%81.ogg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=YCAJEpmZKNWc7mXWszkSkTE2E%2F20251215%2Fru-central1%2Fs3%2Faws4_request&X-Amz-Date=20251215T201130Z&X-Amz-Expires=86400&X-Amz-Signature=8c1f4f9c6437c679d2e6d4a3b5009d16c12624b9622d9da44928e8247def360c&X-Amz-SignedHeaders=host&response-content-disposition=attachment";

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
