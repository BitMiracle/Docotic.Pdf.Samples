# Add new PDF pages in C# and VB.NET

This sample shows how to add new pages to a PDF document using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use the [PdfDocument.Pages](https://api.docotic.com/pdfdocument-pages) property to access the collection of document pages. Call the collection's `Add` method to append a new page to the end of the document, or use `Insert` to add a page at a specified position.

The `index` argument of the `Insert` method must be between `0` and `Pages.Count`, inclusive. Passing `Pages.Count` is equivalent to calling `Add`.

Both `Add` and `Insert` return the newly created page.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add and insert pages](https://bitmiracle.com/pdf-library/edit/#insert-pages)