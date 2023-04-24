# Add header and footer to PDF documents in C# and VB.NET
This sample shows how to add header and footer to PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

You can use PageLayout.Header and PageLayout.Footer methods to add common headers and footers to PDF pages.
Use TextContainer.CurrentPageNumber method to add a page number to the current page.

The library also provides ways to add common headers and footers within page body:
* Use LayoutContainer.Table method to add a table. Then, use Table.Header() and Footer() methods.
* Use LayoutContainer.Decoration method to add a decoration container. Then, use Decoration.Before() and Decoration.After() methods.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Add tables to PDF documents in C# and VB.NET](/Samples/Layout/Tables)
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)