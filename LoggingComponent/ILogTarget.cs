namespace LoggingComponent;

/// <summary>
/// This interface represents a log target. It defines a method for writing a log message.
/// </summary>
public interface ILogTarget
{
    Task WriteLog(LogLevel level, string message);
}