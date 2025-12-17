using Hackathon.Ai;
using Sprache;
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
        public async Task<string> TryDecodeAudioAsync(string audioUri)
        {
            var recognizedText = await speechClient.RecognizeAudioAsync(audioUri);
            return recognizedText;
        }
        public async Task<string> TrySplitByRoleAsync(string text)
        {
            var result = await gptClient.GetGptResponseAsync(text, "Определение дикторов в результатах распознавания. Говорит 'оператор' и 'клиент'. Ты должен перед каждым началом фразы приписать кто ее говорит.");
            return result;
        }

        public async Task<string> TryGetPersonalDataAsync(string text)
        {
            var personalData = await gptClient.GetGptResponseAsync(text, "Выдели (если есть) продиктованные клиентом персональные данные (адрес, номер счета и т.п.), не искажай смысл.");
            return personalData;
        }
    }
}
