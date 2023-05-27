public class MiniLoggerTests
{
    private readonly Mock<ILogTarget> _logTargetMock;
    private readonly MiniLogger _logger;

    public MiniLoggerTests()
    {
        _logTargetMock = new Mock<ILogTarget>();
        _logger = new MiniLogger(LogLevel.Info, _logTargetMock.Object);
    }

    [Fact]
    public async Task Log_GivenLogLevelLowerThanMin_DoesNotWriteLog()
    {
        // Act
        await _logger.Log(LogLevel.Debug, "Test message");

        // Assert
        _logTargetMock.Verify(t => t.WriteLog(It.IsAny<LogLevel>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Log_GivenLogLevelEqualToMin_WritesLog()
    {
        // Act
        await _logger.Log(LogLevel.Info, "Test message");

        // Assert
        _logTargetMock.Verify(t => t.WriteLog(LogLevel.Info, It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Log_GivenLogLevelHigherThanMin_WritesLog()
    {
        // Act
        await _logger.Log(LogLevel.Error, "Test message");

        // Assert
        _logTargetMock.Verify(t => t.WriteLog(LogLevel.Error, It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Log_GivenObject_CallsSerializerAndWritesLog()
    {
        // Arrange
        var testObj = new { Name = "Test" };

        // Act
        await _logger.Log(LogLevel.Info, testObj);

        // Assert
        _logTargetMock.Verify(t => t.WriteLog(LogLevel.Info, It.Is<string>(s => s.Contains(testObj.Name))), Times.Once);
    }

    [Fact]
    public async Task Log_GivenContextData_IncludesDataInLogMessage()
    {
        // Arrange
        var contextData = new Dictionary<string, object> { { "Key", "Value" } };

        // Act
        await _logger.Log(LogLevel.Info, "Test message", contextData);

        // Assert
        _logTargetMock.Verify(t => t.WriteLog(LogLevel.Info, It.Is<string>(s => s.Contains(contextData["Key"].ToString()))), Times.Once);
    }

    [Fact]
    public async Task Log_GivenMultipleLogTargets_WritesToAllTargets()
    {
        // Arrange
        var additionalLogTargetMock = new Mock<ILogTarget>();
        var logger = new MiniLogger(LogLevel.Info, _logTargetMock.Object, additionalLogTargetMock.Object);
        var message = "Test message";

        // Act
        await logger.Log(LogLevel.Info, message);

        // Assert
        _logTargetMock.Verify(t => t.WriteLog(LogLevel.Info, It.IsAny<string>()), Times.Once);
        additionalLogTargetMock.Verify(t => t.WriteLog(LogLevel.Info, It.IsAny<string>()), Times.Once);
    }    
}
