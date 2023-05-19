namespace LoggerAspNet.MyLogger
{
    public class MyLogger : ILogger
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            File.AppendAllText($"MyLog.txt", $"type: {logLevel}. {DateTime.Now:g}. message: {formatter(state, exception)} \n");

        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

    }
}
