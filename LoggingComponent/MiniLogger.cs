using System.Text.Json;

namespace LoggingComponent;

/// <summary>
/// This class represents a simple logger. It supports different log levels and log targets.
/// </summary>
public class MiniLogger : ILogger
{
    private readonly LogLevel _minLogLevel;
    private readonly ILogTarget[] _logTargets;

    /// <summary>
    /// Constructs a new MiniLogger instance.
    /// </summary>
    /// <param name="minLogLevel">The minimum log level. Logs with a level lower than this will not be logged.</param>
    /// <param name="logTargets">The targets where the log messages will be written to.</param> 
    public MiniLogger(LogLevel minLogLevel, params ILogTarget[] logTargets)
    {
        _minLogLevel = minLogLevel;
        _logTargets = logTargets;
    }

    /// <summary>
    /// Logs a message.
    /// </summary>
    /// <param name="level">The level of the log.</param>
    /// <param name="message">The log message.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Log(LogLevel level, string message)
    {
        await Log(level, message, null);
    }

    /// <summary>
    /// Logs an object.
    /// </summary>
    /// <param name="level">The level of the log.</param>
    /// <param name="obj">The object to be logged.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Log(LogLevel level, object obj)
    {
        var serializedObj = JsonSerializer.Serialize(obj);
        await Log(level, serializedObj, null);
    }

    /// <summary>
    /// Logs a message and contextual data.
    /// </summary>
    /// <param name="level">The level of the log.</param>
    /// <param name="message">The log message.</param>
    /// <param name="contextData">Additional contextual data related to the log.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Log(LogLevel level, string message, Dictionary<string, object>? contextData)
    {
        if (level < _minLogLevel)
            return;

        var contextDataSerialized = contextData != null ? JsonSerializer.Serialize(contextData) : "";
        var logEntry = $"{DateTime.UtcNow:O} [{level}] {message} {contextDataSerialized}";

        foreach (var target in _logTargets)
        {
            await target.WriteLog(level, logEntry);
        }
    }
}