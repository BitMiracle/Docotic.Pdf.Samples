# Add header and footer to PDF documents in C# and VB.NET
This sample shows how to add header and footer to PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

You can use [PageLayout.Header](https://bitmiracle.com/pdf-library/help/pagelayout.header.html) and
[PageLayout.Footer](https://bitmiracle.com/pdf-library/help/pagelayout.footer.html) methods to add common headers and footers to PDF pages.
Use [TextContainer.CurrentPageNumber method](https://bitmiracle.com/pdf-library/help/textcontainer.currentpagenumber.html)
to add a page number to the current page.

The library also provides ways to add common headers and footers within page body:
* Use [LayoutContainer.Table method](https://bitmiracle.com/pdf-library/help/layoutcontainer.table.html) to add a table. Then, use `Table.Header()` and `Table.Footer()` methods.
* Use [LayoutContainer.Column method](https://bitmiracle.com/pdf-library/help/layoutcontainer.column.html) to add a column. Then, use `Column.Header()` and `Column.Footer()` methods.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf.aspx) article
* [Page layout in PDF documents](/Samples/Layout/Pages)
* [Add tables to PDF documents in C# and VB.NET](/Samples/Layout/Tables)
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)