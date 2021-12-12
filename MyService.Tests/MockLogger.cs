using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace MyService.Tests
{
    public class MockLogger : ILogger
    {
        readonly Stack<ReceivedLogEvent> _events = new();

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _events.Push(new ReceivedLogEvent {Level = logLevel, Message = state.ToString(), Exception = exception});
        }

        public bool ReceivedNTimes(int expectedEventCount, LogLevel level, string[] messageSubStrings)
        {
            var numMatchedEvents = (from e in _events 
                                      where e.Level == level 
                                      select messageSubStrings.Count(substring => e.Message.Contains(substring))).Count(numMatchesForSubString => numMatchesForSubString == messageSubStrings.Length);

            return numMatchedEvents == expectedEventCount;
        }
    }

    internal class ReceivedLogEvent
    {
        public LogLevel Level { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
