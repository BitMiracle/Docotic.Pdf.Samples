# PDF page layout settings
This sample shows how to customize PDF page settings using [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/).

Use the [Document.Pages method](https://api.docotic.com/layout/document-pages) to define layout for PDF pages.
You can call this method multiple times to generate different page groups.

Use the methods of the [PageLayout](https://api.docotic.com/layout/pagelayout) class to set page properties -
size, margins, background color, text style and text direction.

The `PageLayout` class provides the following methods to generate page content:
* `Background` - a background layer covered by other content
* `Header` - a common header for all pages
* `Content` - a main page content
* `Footer` - a common footer for all pages
* `Foreground` - a foreground layer that covers other content

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [C# PDF generation quick start guide](https://bitmiracle.com/pdf-library/layout/getting-started)
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf)
* [Add header and footer to PDF documents](/Samples/Layout/HeaderFooter)
* [Create multi-column PDF documents in C# and VB.NET](/Samples/Layout/RowsColumns)