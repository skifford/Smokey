using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Smokey.Demo.MSTest.DI.Logger
{
    internal class CustomLogger : ILogger
    {
        private static readonly object Locker = new();
        private static readonly HashSet<LogLevel> AvailableLogLevels = new()
        {
             LogLevel.Information,
             LogLevel.Warning,
             LogLevel.Error,
             LogLevel.Debug
        };

        public IDisposable BeginScope<TState>(TState state) => throw new NotImplementedException();

        public bool IsEnabled(LogLevel logLevel) => AvailableLogLevels.Contains(logLevel);

        public void Log<TState>(LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Information:
                    Log(state, ConsoleColor.White);
                    break;
                case LogLevel.Warning:
                    Log(state, ConsoleColor.DarkYellow);
                    break;
                case LogLevel.Error:
                    Log(state, ConsoleColor.Red);
                    break;
                case LogLevel.Debug:
                    Log(state, ConsoleColor.DarkGray);
                    break;
                default:
                    throw new NotImplementedException(logLevel.ToString());
            }
        }

        private static void Log<TState>(TState state, ConsoleColor color)
        {
            lock (Locker)
            {
                Console.ForegroundColor = color;
                Console.Error.WriteLine(state.ToString().WithDateTime());
                Console.ResetColor();
            }
        }
    }
}