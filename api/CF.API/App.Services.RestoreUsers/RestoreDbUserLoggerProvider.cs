using Microsoft.Extensions.Logging;

namespace App.Services.RestoreUsers;

public class RestoreDbUserLoggerProvider : ILoggerProvider {
    private readonly string _fileLogName = Guid.NewGuid().ToString("N") + $"_{DateTime.Now:yyyy_MM_dd__h_mm_ss_zz}";

    public ILogger CreateLogger(string categoryName) {
        return new RestoreLogger(_fileLogName);
    }

    public void Dispose() {
    }

    private class RestoreLogger : ILogger, IDisposable {
        private readonly string _fileLogName;

        public RestoreLogger(string categoryName) {
            _fileLogName = categoryName;
        }

        public void Dispose() {
        }

        public IDisposable BeginScope<TState>(TState state) {
            return this;
        }

        public bool IsEnabled(LogLevel logLevel) {
            return true;
        }

        public void Log<TState>(LogLevel                         logLevel,
                                EventId                          eventId,
                                TState                           state,
                                Exception?                       exception,
                                Func<TState, Exception?, string> formatter) {
            File.AppendAllText($"Log_CF_Db_Restore_{_fileLogName}.txt", formatter(state, exception));
            Console.WriteLine(formatter(state, exception));
        }
    }
}