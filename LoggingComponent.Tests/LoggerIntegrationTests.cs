public class LoggerIntegrationTests : IDisposable
{
    private const string TestDirectoryPath = "./logs/";
    private const string TestFileName = "integration-test-log.txt";
    private const string TestFilePath = TestDirectoryPath + TestFileName;

    private readonly MiniLogger _logger;

    public LoggerIntegrationTests()
    {
        // Cleanup before starting
        Cleanup();

        // Arrange
        var consoleTarget = new ConsoleLogTarget();
        var fileTarget = new FileLogTarget(TestDirectoryPath, TestFileName);
        _logger = new MiniLogger(LogLevel.Debug, consoleTarget, fileTarget);
    }

    [Fact]
    public async Task Log_WritesMessageToAllTargets()
    {
        // Act
        var message = "Integration test message";
        await _logger.Log(LogLevel.Info, message);

        // Assert
        // Assumption that console output was correct
        // For file target, we read the file and check if the message was written correctly
        var logContent = await LogReader.ReadLog(TestFilePath);
        Assert.Contains(message, logContent);
    }

    public void Dispose()
    {
        Cleanup();
    }

    private static void Cleanup()
    {
        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }
    }
}
