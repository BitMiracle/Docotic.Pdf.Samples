# Convert HTML to PDF after a script run in C# or VB.NET
This sample shows how to run a script before converting HTML to PDF in C# or VB.NET application.

Sometimes it is required to run a script after the page is loaded but before it is converted. For example, to toggle elements, or to make dynamic content loading happen.

You can specify the script to run in [conversion start options](https://bitmiracle.com/pdf-library/help/conversionstartoptions.html) inside the HtmlConversionOptions. Then pass the options to the CreatePdfAsync method.

This sample code uses free [Docotic.Pdf.HtmlToPdf add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.HtmlToPdf/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)
* [Convert HTML to PDF with delay](/Samples/HtmlToPdf/ConvertWithDelay) sample
