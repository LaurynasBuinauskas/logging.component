namespace LoggingComponent;

/// <summary>
/// This class provides a method for reading a log file.
/// </summary>
public static class LogReader
{
    public static async Task<string> ReadLog(string filePath)
    {
        var content = await File.ReadAllTextAsync(filePath);
        return content;
    }
}