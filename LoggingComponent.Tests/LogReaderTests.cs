public class LogReaderTests
{
    [Fact]
    public async Task ReadLog_ReturnsCorrectContent()
    {
        // Arrange
        var filePath = "test-log.txt";
        var expectedContent = "Test message";
        await File.WriteAllTextAsync(filePath, expectedContent);
        

        // Act
        var actualContent = await LogReader.ReadLog(filePath);

        // Assert
        Assert.Equal(expectedContent, actualContent);

        // Clean up
        File.Delete(filePath);
    }

    [Fact]
    public async Task ReadLog_ThrowsException_WhenFileDoesNotExist()
    {              
        // Act and Assert
        await Assert.ThrowsAsync<FileNotFoundException>(() => LogReader.ReadLog("non-existing-file.txt"));
    }
}