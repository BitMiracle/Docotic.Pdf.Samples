# Error handling

This sample shows how to handle PDF-specific exceptions thrown by [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) in C# and VB.NET.

## Description

Besides standard .NET exceptions (such as `ArgumentNullException` or `ArgumentOutOfRangeException`) the library throws exceptions of the [PdfException](https://api.docotic.com/pdfexception) type and its subtypes. These exceptions are specific to PDF format. 

The library throws them when you try to do something that is not allowed or invalid from the PDF standard point of view. This sample shows one of such situations: a try to draw some text with characters which are not supported by the current font.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Logging with log4net](/Samples/General%20operations/LoggingWithLog4Net) sample
* [Logging with NLog](/Samples/General%20operations/LoggingWithNLog) sample