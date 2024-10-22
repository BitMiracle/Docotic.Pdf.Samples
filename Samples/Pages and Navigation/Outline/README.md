# Build PDF outline (bookmarks) in C# and VB.NET

This sample shows how to build a PDF document bookmarks using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

Docotic.Pdf library provides methods and properties to manipulate document’s outline. The [PdfDocument.OutlineRoot](https://api.docotic.com/pdfdocument-outlineroot) property provides access to the document’s outline root. The value of this property is a [PdfOutlineItem](https://api.docotic.com/pdfoutlineitem) object. All outline items are instances of this class. Use methods and properties of the `PdfOutlineItem` class to access and set up outline items.

You can use [PdfOutlineItem.AddChild](https://api.docotic.com/pdfoutlineitem-addchild) methods to add child items to an outline item. To create an outline, start by adding child items to the outline root. 

To instruct PDF viewers to show "Bookmarks" panel when your document is opened, set the [PdfDocument.PageMode](https://api.docotic.com/pdfdocument-pagemode) property to the `PdfPageMode.UseOutlines` value.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [PDF bookmarks](https://bitmiracle.com/pdf-library/edit/#bookmarks)