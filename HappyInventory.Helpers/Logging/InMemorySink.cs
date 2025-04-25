using Serilog.Core;
using Serilog.Events;

namespace HappyInventory.Helpers.Logging
{
    public class InMemorySink : ILogEventSink
    {
        private readonly List<LogEvent> _logEvents;

        public InMemorySink(List<LogEvent> logEvents)
        {
            _logEvents = logEvents;
        }

        public void Emit(LogEvent logEvent)
        {
            _logEvents.Add(logEvent);
        }
    }
}
