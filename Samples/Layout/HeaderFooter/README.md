# Add header and footer to PDF documents in C# and VB.NET
This sample shows how to add header and footer to PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

You can use the [PageLayout.Header](https://api.docotic.com/layout/pagelayout-header) and
[PageLayout.Footer](https://api.docotic.com/layout/pagelayout-footer) methods to add common headers and footers to PDF pages.
Use the [TextContainer.CurrentPageNumber method](https://api.docotic.com/layout/textcontainer-currentpagenumber)
to add a page number to the current page.

The library also provides ways to add common headers and footers within page body:
* Use the [LayoutContainer.Table method](https://api.docotic.com/layout/layoutcontainer-table) to add a table. Then, use the `Table.Header` and `Table.Footer` methods.
* Use the [LayoutContainer.Column method](https://api.docotic.com/layout/layoutcontainer-column) to add a column. Then, use the `Column.Header` and `Column.Footer` methods.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [C# PDF generation quick start guide](https://bitmiracle.com/pdf-library/layout/getting-started)
* [How to layout PDF pages](https://bitmiracle.com/pdf-library/layout/pages)
* [Compound containers](https://bitmiracle.com/pdf-library/layout/compounds)
* [Table container](https://bitmiracle.com/pdf-library/layout/table)
* [Page layout in PDF documents](/Samples/Layout/Pages) example
* [Add tables to PDF documents in C# and VB.NET](/Samples/Layout/Tables) example