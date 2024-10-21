# Add tables to PDF documents in C# and VB.NET
This sample shows how to create tables in PDF documents using [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/).

You can use the [LayoutContainer.Table method](https://api.docotic.com/layout/layoutcontainer-table)
to add a table. Set table columns using the [Table.Columns method](https://api.docotic.com/layout/table-columns).

Use [Table.Cell methods](https://api.docotic.com/layout/table-cell) to add cells to the table body.
Cells can cover multiple columns or rows, use the [TableCell.ColumnSpan](https://api.docotic.com/layout/tablecell-columnspan) and [TableCell.RowSpan](https://api.docotic.com/layout/tablecell-rowspan) methods to set up spans.

Use the [Table.Header method](https://api.docotic.com/layout/table-header) to define table header.
Similarly, you can use the [Table.Footer method](https://api.docotic.com/layout/table-footer) to define table footer.
When table content does not fit on a single PDF page, the library repeats the table header and footer on all pages.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [C# PDF generation quick start guide](https://bitmiracle.com/pdf-library/layout/getting-started)
* [How to layout PDF pages](https://bitmiracle.com/pdf-library/layout/pages)
* [Table container](https://bitmiracle.com/pdf-library/layout/table)
* [Create multi-column PDF documents](/Samples/Layout/RowsColumns) example
* [Generate PDF documents with complex layout](/Samples/Layout/ComplexLayout) example