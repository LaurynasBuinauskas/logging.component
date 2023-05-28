User Guide for the LoggingComponent Library

The LoggingComponent library offers simple and extensible logging capabilities for .NET applications. This user guide will explain how to use the main components of the library.

Link to the NuGet package: https://www.nuget.org/packages/LoggingComponent_Laurynas_Komponentas/.

Core Components:

-> ILogger interface: Defines the contract for logger implementations. Supports logging of simple messages,        structured data, and contextual information.

-> ILogTarget interface: Defines the contract for log targets, which are the outputs where the log messages are    written to.

-> MiniLogger: A basic implementation of ILogger. Allows setting a minimum log level and multiple log targets.

-> ConsoleLogTarget: An implementation of ILogTarget that writes log messages to the console.

-> FileLogTarget: An implementation of ILogTarget that writes log messages to a specified file.

-> LogReader: A utility class for reading log files.

-> LogLevel: An enumeration defining the available log levels.


Steps to Use the Library:

Instantiate a Log Target:
Choose a log target that matches where you want your log messages to be written to. You can choose from ConsoleLogTarget and FileLogTarget -> 
var consoleTarget = new ConsoleLogTarget();

Specify the directory path and the file name as arguments to the constructor -> 
var fileTarget = new FileLogTarget(@"C:\Logs\", "log.txt");


Instantiate a Logger:
Create an instance of MiniLogger and specify the minimum log level and the log targets in the constructor. Only messages with a log level equal to or higher than the specified minimum will be logged ->
ILogger logger = new MiniLogger(LogLevel.Info, consoleTarget, fileTarget);


Log a Message:
Call the Log method on your logger instance and provide a log level and a message ->
await logger.Log(LogLevel.Info, "This is an info message.");


Log an Object:
You can also log structured data by passing an object ->
var myObject = new { Name = "John", Age = 30 };
await logger.Log(LogLevel.Debug, myObject);


Log Contextual Data:
Additionally, you can pass a Dictionary<string, object> to provide contextual data ->
var contextData = new Dictionary<string, object> { { "UserId", 1 }, { "Action", "Update" } };
await logger.Log(LogLevel.Debug, "User action", contextData);

Read a Log File:
If you're using FileLogTarget, you can read a log file using LogReader ->
var logContent = await LogReader.ReadLog(@"C:\Logs\log.txt");


Customization:
You can create your own logger or log target by implementing ILogger or ILogTarget, respectively. This allows you to extend the library to support additional log levels, log formats, or log targets.

The ILogTarget implementation should handle how to format and write the log message. The ILogger implementation should handle how to filter log messages based on their level and how to dispatch log messages to the different targets.
