namespace LoggingComponent;

/// <summary>
/// This interface represents a logger. It defines methods for logging messages, objects, and contextual data.
/// </summary>
public interface ILogger
{
    Task Log(LogLevel level, string message);
    Task Log(LogLevel level, object obj);
    Task Log(LogLevel level, string message, Dictionary<string, object> contextData);
}