# Convert HTML and overlay it on existing PDF content in C# or VB.NET

This sample shows how to convert HTML to PDF and overlay the resulting content on existing PDF pages in a C# or VB.NET application.

See the [Overlay HTML onto an existing PDF](https://bitmiracle.com/pdf-library/html-pdf/#overlay-html) section for more information.

You can overlay one PDF page on another by [creating an XObject from the source page](/Samples%20/XObjects%2C%20watermarks%20and%20backgrounds/CreateXObjectFromPage) and drawing it on the target page. The source and target pages can belong to different PDF documents.

This sample uses the same technique to overlay converted HTML on existing PDF content. First, it converts the HTML into PDF pages with transparent backgrounds. Then, it creates XObjects from those pages and draws them on the existing PDF pages.

This sample uses the free [Docotic.Pdf.HtmlToPdf add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.HtmlToPdf/) for the Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [HTML to PDF for .NET](https://bitmiracle.com/pdf-library/html-pdf/)
* [Convert HTML to PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/html-pdf/convert)
* [Create XObject from page](/Samples/XObjects%2C%20watermarks%20and%20backgrounds/CreateXObjectFromPage) sample