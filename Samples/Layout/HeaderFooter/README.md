# Add header and footer to PDF documents in C# and VB.NET
This sample shows how to add header and footer to PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

You can use [PageLayout.Header](https://bitmiracle.com/pdf-library/api/layout/pagelayout-header) and
[PageLayout.Footer](https://bitmiracle.com/pdf-library/api/layout/pagelayout-footer) methods to add common headers and footers to PDF pages.
Use [TextContainer.CurrentPageNumber method](https://bitmiracle.com/pdf-library/api/layout/textcontainer-currentpagenumber)
to add a page number to the current page.

The library also provides ways to add common headers and footers within page body:
* Use [LayoutContainer.Table method](https://bitmiracle.com/pdf-library/api/layout/layoutcontainer-table) to add a table. Then, use `Table.Header()` and `Table.Footer()` methods.
* Use [LayoutContainer.Column method](https://bitmiracle.com/pdf-library/api/layout/layoutcontainer-column) to add a column. Then, use `Column.Header()` and `Column.Footer()` methods.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [C# PDF generation quick start guide](https://bitmiracle.com/pdf-library/layout/getting-started)
* [How to layout PDF pages](https://bitmiracle.com/pdf-library/layout/pages)
* [Compound containers](https://bitmiracle.com/pdf-library/layout/compounds)
* [Table container](https://bitmiracle.com/pdf-library/layout/table)
* [Page layout in PDF documents](/Samples/Layout/Pages) example
* [Add tables to PDF documents in C# and VB.NET](/Samples/Layout/Tables) example
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)