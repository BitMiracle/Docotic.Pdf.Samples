# Logging with NLog

This sample shows how to receive log messages from [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) into a NLog logger.

## Description

Start with configuring NLog. This is required to receive log messages from the library. It is a common practice to put NLog configuration into `app.config` file. 

This example uses the `NLog.Extensions.Logging` package to create an instance of the `NLogLoggerFactory` class. This class implements the `ILoggerFactory` interface. The code provides the instance to the [LogManager](https://api.docotic.com/logging/logmanager) class from the [Docotic.Logging add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Logging/).

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Logging with log4net](/Samples/General%20operations/LoggingWithLog4Net) sample
* [Error handling](/Samples/General%20operations/ErrorHandling) sample
