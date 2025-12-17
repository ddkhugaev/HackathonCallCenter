using Hackathon.Ai.Models;

namespace HackathonCallCenter
{
    public interface ICallAnalyzerService
    {
        Task<CallAnalysisResult> AnalyzeCallAsync(string callTranscript);
    }
}