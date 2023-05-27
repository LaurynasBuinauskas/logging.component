namespace LoggingComponent;

/// <summary>
/// This class represents a console-based log target. It is used to write log messages to the console.
/// </summary>
public class ConsoleLogTarget : ILogTarget
{
    /// <summary>
    /// Writes a log message to the console.
    /// </summary>
    /// <param name="level">The level of the log.</param>
    /// <param name="message">The log message.</param>
    /// <returns>A completed task.</returns>
    public Task WriteLog(LogLevel level, string message)
    {       
        Console.WriteLine(message);
        return Task.CompletedTask;
    }
}