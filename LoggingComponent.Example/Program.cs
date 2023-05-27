using LoggingComponent;

// Start of the first example:
// Instantiate a ConsoleLogTarget. This will enable our logger to write logs to the console.
var consoleTarget = new ConsoleLogTarget();

// Instantiate a FileLogTarget. This will enable our logger to write logs to the file located at C:\Logs\log.txt.
// If the specified file doesn't exist, it will be created.
var fileTarget = new FileLogTarget(@"C:\Logs\", "log.txt");

// Create a MiniLogger, passing LogLevel.Info as the minimum log level and the console and file targets.
// This means the logger will only log messages with level Info or higher and will write logs to both the console and the file.
ILogger logger = new MiniLogger(LogLevel.Info, consoleTarget, fileTarget);

// Log an info message. This will be logged because Info is at or above the logger's minimum log level.
await logger.Log(LogLevel.Info, "This is an info message.");

// Log a warning message. This will be logged because Warning is at or above the logger's minimum log level.
await logger.Log(LogLevel.Warning, "This is a warning message.");

// Log an error message. This will be logged because Error is at or above the logger's minimum log level.
await logger.Log(LogLevel.Error, "This is an error message.");

// Log an error message with additional context
var errorContext = new Dictionary<string, object> { { "cause", "database connection failed" }, { "retryAttempt", 3 } };
await logger.Log(LogLevel.Error, "This is an error message with context.", errorContext);

// Log a debug message with an object. This won't be logged because Debug is below the logger's minimum log level.
var myObject = new { Name = "John", Age = 30 };
await logger.Log(LogLevel.Debug, myObject);

// Use LogReader to read the content of the log file and print it to the console.
var logContent = await LogReader.ReadLog(@"C:\Logs\log.txt");
Console.WriteLine("Log file content:");
Console.WriteLine(logContent);

// Start of the second example:
// Instantiate a new FileLogTarget. This will enable our new logger to write logs to the file located at C:\Logs\log2.txt.
var newFileTarget = new FileLogTarget(@"C:\Logs\", "log2.txt");

// Create a new MiniLogger, passing LogLevel.Debug as the minimum log level and the console and new file targets.
// This means the new logger will log all messages as Debug is the lowest log level.
ILogger newLogger = new MiniLogger(LogLevel.Debug, consoleTarget, newFileTarget);

// Log a debug message with an object. This will be logged because Debug is at or above the new logger's minimum log level.
var newObject = new { Name = "Tom", Age = 33 };
await newLogger.Log(LogLevel.Debug, newObject);

// Log a warning message. This will be logged because Warning is at or above the new logger's minimum log level.
await newLogger.Log(LogLevel.Warning, "This is a second warning message.");

// Use LogReader to read the content of the second log file and print it to the console.
var SecondLogContent = await LogReader.ReadLog(@"C:\Logs\log2.txt");
Console.WriteLine("Second log file content:");
Console.WriteLine(SecondLogContent);

