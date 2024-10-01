using System;
using Microsoft.Extensions.Logging;

namespace TS.Configuration
{
    /// <inheritdoc />
    public class LogService : ILogService
    {
        private readonly ILoggerFactory _loggerFactory;

        public LogService(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        /// <inheritdoc />
        public void LogDebug(string category, string msg)
        {
            var logger = _loggerFactory.CreateLogger(category);
            logger.LogDebug(msg);
        }

        /// <inheritdoc />
        public void LogDebug<T>(string msg)
        {
            var logger = _loggerFactory.CreateLogger<T>();
            logger.LogDebug(msg);
        }

        /// <inheritdoc />
        public void LogInfo(string category, string msg)
        {
            var logger = _loggerFactory.CreateLogger(category);
            logger.LogInformation(msg);
        }

        /// <inheritdoc />
        public void LogInfo<T>(string msg)
        {
            var logger = _loggerFactory.CreateLogger<T>();
            logger.LogInformation(msg);
        }

        /// <inheritdoc />
        public void LogError(string category, Exception exception, string message)
        {
            var logger = _loggerFactory.CreateLogger(category);
            logger.LogError(message + exception.InnerException.Message.ToString());
        }

        /// <inheritdoc />
        public void LogError<T>(Exception exception, string message)
        {
            var logger = _loggerFactory.CreateLogger<T>();
            if (exception.InnerException != null)
              message += exception.InnerException.Message;
            logger.LogError(message);
        }

        /// <inheritdoc />
        public void LogError(string category, Exception exception)
        {
            var logger = _loggerFactory.CreateLogger(category);
            var message = exception.Message;
            if (exception.InnerException != null)
              message += exception.InnerException.Message;
            logger.LogError(exception, message);
        }
        
        /// <inheritdoc />
        public void LogError<T>(Exception exception)
        {
            var logger = _loggerFactory.CreateLogger<T>();
            var message = exception.Message;
            if (exception.InnerException != null)
              message += exception.InnerException.Message;
            logger.LogError(exception, message);
        }

        /// <inheritdoc />
        public void LogError(string category, Exception exception, string template, string message, params object[] args)
        {
            var logger = _loggerFactory.CreateLogger(category);
            if (exception.InnerException != null)
              message += exception.InnerException.Message;
            logger.LogError(exception, template, message, args);
        }

        /// <inheritdoc />
        public void LogError<T>(Exception exception, string template, string message, params object[] args)
        {
            var logger = _loggerFactory.CreateLogger<T>();
            if (exception.InnerException != null)
              message += exception.InnerException.Message;
            logger.LogError(exception, template, message, args);
        }

        /// <inheritdoc />
        public void LogError(string category, string message, params object[] args)
        {
            var logger = _loggerFactory.CreateLogger(category);
            logger.LogError(message, args);
        }

        /// <inheritdoc />
        public void LogError<T>(string message, params object[] args)
        {
            var logger = _loggerFactory.CreateLogger<T>();
            logger.LogError(message, args);
        }
    }
}