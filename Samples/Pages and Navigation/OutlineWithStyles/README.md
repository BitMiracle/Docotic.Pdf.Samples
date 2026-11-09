# Change font and color of PDF bookmarks in C# and VB.NET

This sample shows how to change the color and font style of PDF bookmarks using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Docotic.Pdf provides methods and properties for working with PDF document bookmarks. The `PdfDocument.Outlines` property gives access to the collection of top-level bookmarks. Each bookmark is represented by an instance of the [PdfOutlineItem](https://api.docotic.com/pdfoutlineitem) class and can contain child bookmarks accessible through its `Children` collection.

Enumerate the bookmarks in the `PdfDocument.Outlines` and `PdfOutlineItem.Children` collections to change their appearance. Use the `PdfOutlineItem.Bold`, `PdfOutlineItem.Italic`, and `PdfOutlineItem.Color` properties to make bookmarks bold, italic, or colored.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [PDF bookmarks](https://bitmiracle.com/pdf-library/edit/#bookmarks)
