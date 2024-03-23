# Convert HTML and put it over existing PDF content in C# or VB.NET
This sample shows how to convert HTML and put it over existing PDF content in C# or VB.NET application.

It is possible to put the content of one PDF page on top of another one(-s). You can [create an XObject](/Samples/XObjects%2C%20watermarks%20and%20backgrounds/CreateXObjectFromPage) from the source page and then draw the XObject on any number of other pages. Source and target pages can belong to different documents. 

So, to put converted HTML over existing PDF content, you need to create PDF pages with transparent background from the HTML, then create XObjects from the converted pages, and then draw those XObjects on existing PDF pages.

This sample code uses free [Docotic.Pdf.HtmlToPdf add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.HtmlToPdf/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Convert HTML to PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/html-pdf/convert) article