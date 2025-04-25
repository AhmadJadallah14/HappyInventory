using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using System.Diagnostics;

namespace HappyInventory.Helpers.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;
        private const string LogCacheKey = "LogCache";

        public LoggingMiddleware(RequestDelegate next, IMemoryCache memoryCache)
        {
            _next = next;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var stopwatch = Stopwatch.StartNew();
            var request = httpContext.Request;
            var response = httpContext.Response;

            Log.Information("Request: {Method} {Url} at {Time}", request.Method, request.Path, stopwatch.Elapsed);

            try
            {
                await _next(httpContext);

                Log.Information("Response: {StatusCode} at {Time}", response.StatusCode, stopwatch.Elapsed);
                AddLogToCache($"INFO: Request {request.Method} {request.Path} responded with {response.StatusCode}.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing the request.");
                AddLogToCache($"ERROR: {ex.Message}");
                throw;  
            }
            finally
            {
                stopwatch.Stop();
            }
        }
        private void AddLogToCache(string logMessage)
        {
            var logs = _memoryCache.Get<List<string>>(LogCacheKey) ?? new List<string>();
            logs.Add(logMessage);
            _memoryCache.Set(LogCacheKey, logs);
        }
    }
}
