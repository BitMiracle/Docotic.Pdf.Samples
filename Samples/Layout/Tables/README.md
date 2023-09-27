# Add tables to PDF documents in C# and VB.NET
This sample shows how to create tables in PDF documents using [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/).

You can use [LayoutContainer.Table method](https://bitmiracle.com/pdf-library/api/layout/layoutcontainer-table)
to add a table. Set table columns using [Table.Columns method](https://bitmiracle.com/pdf-library/api/layout/table-columns).

Use [Table.Cell methods](https://bitmiracle.com/pdf-library/api/layout/table-cell) to add cells to the table body.
Cells can cover multiple columns or rows, use `TableCell.ColumnSpan/RowSpan` methods.

Use [Table.Header method](https://bitmiracle.com/pdf-library/api/layout/table-header) to define table header.
Similarly, you can use Use [Table.Footer method](https://bitmiracle.com/pdf-library/api/layout/table-footer) to define table footer.
When table content does not fit to a single PDF page, the library repeats table header and footer on all pages.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf.aspx) article
* [Create multi-column PDF documents](/Samples/Layout/RowsColumns)
* [Generate PDF documents with complex layout](/Samples/Layout/ComplexLayout)
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)