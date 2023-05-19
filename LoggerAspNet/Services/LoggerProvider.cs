namespace LoggerAspNet.Services;

public class LoggerProvider
{
    public List<LoggerBase> Loggers = new List<LoggerBase>()
    {
        new Logger(),
        new SaveLoggerToDatabase()
    };  

    public void Log(string message, LogType type = LogType.info)
    {
        foreach (var logger in Loggers) 
        {
            logger.Log(message, type);
        }
    }
}
