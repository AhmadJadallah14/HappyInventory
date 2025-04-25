using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace HappyInventory.Services.LogService
{

    public class LogsService : ILogsService
    {
        private readonly IMemoryCache _memoryCache;
        private const string LogCacheKey = "LogCache";

        public LogsService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void LogInformation(string message)
        {
            Log.Information(message);
            AddLogToCache($"INFO: {message}");
        }

        public void LogError(Exception exception, string message)
        {
            Log.Error(exception, message);
            AddLogToCache($"ERROR: {message} - Exception: {exception?.Message}");
        }

        public void LogWarning(string message)
        {
            Log.Warning(message);
            AddLogToCache($"WARNING: {message}");
        }
        public List<string> GetLogs()
        {
            return _memoryCache.Get<List<string>>(LogCacheKey) ?? new List<string>();
        }

        private void AddLogToCache(string logMessage)
        {
            var logs = _memoryCache.Get<List<string>>(LogCacheKey) ?? new List<string>();
            logs.Add(logMessage);
            _memoryCache.Set(LogCacheKey, logs);
        }
    }
}
