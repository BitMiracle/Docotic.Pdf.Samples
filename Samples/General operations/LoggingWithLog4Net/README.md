# Logging with log4net

This sample shows how to receive log messages from [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) into a log4net logger.

## Description

To receive log messages from the library, you would need to configure log4net first. This code sample uses the `Microsoft.Extensions.Logging.Log4Net.AspNetCore` package. The package configures log4net using the provided config. Look into the `app.config` file, it contains more comments.

When log4net configuration is done, create an `ILoggerFactory` instance and provide the instance to the [LogManager](https://api.docotic.com/logging/logmanager) class from the [Docotic.Logging add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Logging/).

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Logging with NLog](/Samples/General%20operations/LoggingWithNLog) sample
* [Error handling](/Samples/General%20operations/ErrorHandling) sample
