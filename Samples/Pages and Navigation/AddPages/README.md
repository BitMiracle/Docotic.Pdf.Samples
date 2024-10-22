# Add new PDF pages in C# and VB.NET

This sample shows how to add or insert pages into your PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

You can access the collection of PDF document pages using the [PdfDocument.Pages](https://api.docotic.com/pdfdocument-pages) property. Use the [PdfDocument.AddPage](https://api.docotic.com/pdfdocument-addpage) method to add a new page to the end of the document. The [PdfDocument.InsertPage](https://api.docotic.com/pdfdocument-insertpage) method inserts a new page into an arbitrary position in the document. 

Valid values for the `index` argument of the `InsertPage` method are from `0` to `Pages.Count` inclusive. Calling the `InsertPage` method with the `index == Pages.Count` is the same as calling the `AddPage` method. 

Both the `AddPage` and `InsertPage` methods return the newly created page.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add and insert pages](https://bitmiracle.com/pdf-library/edit/#insert-pages)