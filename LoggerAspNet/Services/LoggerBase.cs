namespace LoggerAspNet.Services
{
    public abstract class LoggerBase
    {
        public abstract void Log(string message, LogType type = LogType.info);
    }
}
