using Microsoft.Extensions.Logging;

namespace App.Services.RestoreUsers;

public class RestoreCFUserLoggerProvider : ILoggerProvider {
    private readonly string _fileLogName = Guid.NewGuid().ToString("N") + $"_{DateTime.Now:yyyy_MM_dd__h_mm_ss_zz}";

    public ILogger CreateLogger(string categoryName) {
        return new RestoreLogger(categoryName + "_" + _fileLogName);
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
            var dirName = "Logs";
            var dir = Path.Combine(AppContext.BaseDirectory, dirName);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.AppendAllText(Path.Combine(dirName, $"Log_CF_Restore_{_fileLogName}.txt"), formatter(state, exception));
            Console.WriteLine(formatter(state, exception));
        }
    }
}