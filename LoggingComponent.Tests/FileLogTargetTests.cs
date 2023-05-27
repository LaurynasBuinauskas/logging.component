public class FileLogTargetTests
{
    [Fact]
    public async Task WriteLog_CreatesFileAndWritesToIt()
    {
        // Arrange
        var target = new FileLogTarget(@"./", "test-log-file.txt");
        var message = "Test message";

        // Act
        await target.WriteLog(LogLevel.Info, message);

        // Assert
        Assert.True(File.Exists("test-log.txt"));
        var logContent = await File.ReadAllTextAsync("test-log-file.txt");
        Assert.Contains(message, logContent);

        // Clean up
        File.Delete("test-log.txt");
    }
}