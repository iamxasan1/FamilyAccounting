namespace LoggerAspNet.Services;

public class Logger : LoggerBase
{
    public override void Log(string message, LogType type=LogType.info )
    {
        File.AppendAllText($"log{DateTime.Now:(dd-MM-YYYY MM-ss)}.txt", $"type: {type}. {DateTime.Now:g}. message: {message} \n");

    }
}

public enum LogType
{
    info,
    warning,
    error 
}