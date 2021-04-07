using Microsoft.Extensions.Logging;
using System;

namespace ShoppingProject.Data.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;
        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
        public void Log(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            _logger.Log(logLevel, exception, message, args);
        }
    }
}
