# Create XObject from page

This sample shows how to merge two PDF pages into one page side by side. For this, the code creates an XObject from each page and then draws these objects on a new page side by side.

## Description

[Docotic.Pdf library](https://bitmiracle.com/pdf-library/) can create XObjects from PDF document pages. XObject is a container with graphics, images, and text. For most use cases, such objects are like vector image.

You can reuse the same XObject on many pages without inflating the size of the document. You can put an XObject into another document. Use one of the [PdfCanvas.DrawXObject](https://api.docotic.com/pdfcanvas-drawxobject) methods in your C# or VB.NET code to add the XObject to a page.

XObjects are a good choice for watermarks and backgrounds. You can also use XObjects to impose (combine) PDF pages onto sheets to make books, booklets, or special arrangements. It is possible to resize pages with the help of such objects.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Watermarks & backgrounds](https://bitmiracle.com/pdf-library/edit/#watermarks)
* [Impose PDF](https://bitmiracle.com/pdf-library/edit/merge#impose-pdf)
* [Change page size](https://bitmiracle.com/pdf-library/edit/#resize-pages)
