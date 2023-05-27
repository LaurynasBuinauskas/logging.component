namespace LoggingComponent;

/// <summary>
/// This class represents a file-based log target. It is used to write log messages to a specified file.
/// </summary>
public class FileLogTarget : ILogTarget
{
    private readonly string _filePath;

    /// <summary>
    /// Constructs a new FileLogTarget instance.
    /// </summary>
    /// <param name="directoryPath">The directory of the log file.</param>
    /// <param name="fileName">The name of the log file.</param>
    public FileLogTarget(string directoryPath, string fileName)
    {        
        if (!directoryPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            directoryPath += Path.DirectorySeparatorChar;
        }
        
        _filePath = Path.Combine(directoryPath, fileName);
    }

    /// <summary>
    /// Writes a log message to a file.
    /// </summary>
    /// <param name="level">The level of the log.</param>
    /// <param name="message">The log message.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task WriteLog(LogLevel level, string message)
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (directory != null)
        {
            Directory.CreateDirectory(directory);
        }

        await File.AppendAllTextAsync(_filePath, message + Environment.NewLine);
    }
}