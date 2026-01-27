# Convert HTML to PDF with header and footer in C# or VB.NET
This sample shows how to specify header and footer templates when converting HTML to PDF in C# or VB.NET application.

The [Add header and footer](https://bitmiracle.com/pdf-library/html-pdf/#header-footer) section contains additional information.

## Description

To specify header and/or footer templates you can use the [page options](https://api.docotic.com/htmltopdf/pdfpageoptions) inside the `HtmlConversionOptions`. Then pass the options to the `CreatePdfAsync` method.

You might want to specify top and bottom margins for the page. Without margins, the header or the footer may be obscured by the page contents.

It is recommended to use inline styles and [Data URIs](https://en.wikipedia.org/wiki/Data_URI_scheme) for images.

Please check the documentation for `HeaderTemplate` and `FooterTemplate` properties for the list of supported variables. The templates in this sample show how to use the variables.

This sample code uses free [Docotic.Pdf.HtmlToPdf add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.HtmlToPdf/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [HTML to PDF for .NET](https://bitmiracle.com/pdf-library/html-pdf/)
* [Convert HTML to PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/html-pdf/convert)