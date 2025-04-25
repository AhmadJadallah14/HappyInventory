using HappyInventory.Models.DTOs.Logging;

namespace HappyInventory.Services.LogService
{
    public interface ILogsService
    {
        List<string> GetLogs();
        void LogInformation(string message);
        void LogError(Exception exception, string message);
        void LogWarning(string message);
    }
}