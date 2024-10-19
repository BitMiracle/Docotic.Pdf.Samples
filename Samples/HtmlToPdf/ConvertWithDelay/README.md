# Convert HTML to PDF with delay in C# or VB.NET
This sample shows how to delay an HTML to PDF conversion in C# or VB.NET application.

The [Wait before conversion](https://bitmiracle.com/pdf-library/html-pdf/#after-delay) section contains additional information.

## Description

There are cases when you might want to delay an HTML to PDF conversion. For example, when you want to convert a page, that executes a script after loading. 

For such cases, you can set up the delay in [conversion start options](https://api.docotic.com/htmltopdf/conversionstartoptions) inside the `HtmlConversionOptions`. Then pass the options to the `CreatePdfAsync` method.

This sample code uses free [Docotic.Pdf.HtmlToPdf add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.HtmlToPdf/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Convert HTML to PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/html-pdf/convert)
* [Convert HTML to PDF after a script run](/Samples/HtmlToPdf/ConvertAfterScriptRun) sample