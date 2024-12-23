# Convert HTML to PDF with margins and custom scaling in C# or VB.NET
This sample shows how to set up margins and a scaling factor when converting HTML to PDF in C# or VB.NET application.

The [Scale content](https://bitmiracle.com/pdf-library/html-pdf/#scale) section contains additional information.

## Description

To set up margins in the output PDF you can use the [page options](https://api.docotic.com/htmltopdf/pdfpageoptions) inside the `HtmlConversionOptions`. Then pass the options to the `CreatePdfAsync` method. You can use the same options to set up a scaling factor.

This sample code uses free [Docotic.Pdf.HtmlToPdf add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.HtmlToPdf/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Convert HTML to PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/html-pdf/convert)