# Build PDF outlines (bookmarks) in C# and VB.NET

This sample shows how to create bookmarks in PDF documents using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Docotic.Pdf provides methods and properties for working with PDF document bookmarks. The `PdfDocument.Outlines` property gives access to the collection of top-level bookmarks. Each bookmark is represented by an instance of the [PdfOutlineItem](https://api.docotic.com/pdfoutlineitem) class.

Use the methods and properties of `PdfOutlineItem` to configure bookmarks. For example, set the `PdfOutlineItem.View` property to specify the view that a PDF viewer should display when a bookmark is activated.

To create nested bookmarks, use the `Add` methods of the `PdfOutlineItem.Children` collection. Start by adding bookmarks to the `PdfDocument.Outlines` collection, then add child bookmarks to individual outline items as needed.

To instruct PDF viewers to display the Bookmarks panel when opening the document, set the [PdfDocument.PageMode](https://api.docotic.com/pdfdocument-pagemode) property to `PdfPageMode.UseOutlines`.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [PDF bookmarks](https://bitmiracle.com/pdf-library/edit/#bookmarks)