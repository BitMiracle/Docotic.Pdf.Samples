# PDF page layout settings
This sample shows how to customize PDF page settings using [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/).

Use [Document.Pages method](https://bitmiracle.com/pdf-library/api/layout/document-pages) to define layout for PDF pages.
You can call this method multiple times to generate different page groups.

Use [PageLayout methods](https://bitmiracle.com/pdf-library/api/layout/pagelayout) to set page propeties -
size, margins, background color, text style and text direction.

`PageLayout` class provides the following methods to generate page content:
* `Background()` - a background layer covered by other content
* `Header()` - a common header for all pages
* `Content()` - a main page content
* `Footer()` - a common footer for all pages
* `Foreground()` - a foreground layer that covers other content

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf.aspx) article
* [Add header and footer to PDF documents](/Samples/Layout/HeaderFooter)
* [Create multi-column PDF documents in C# and VB.NET](/Samples/Layout/RowsColumns)
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)