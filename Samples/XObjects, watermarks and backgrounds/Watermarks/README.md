# Add watermarks and backgrounds to PDF documents in C# and VB.NET

This sample shows how to add watermarks and backgrounds to PDF pages using the Docotic.Pdf library in C# or VB.NET applications.

Use the [PdfDocument.CreateXObject](https://api.docotic.com/pdfdocument-createxobject) method to create a [PdfXObject](https://api.docotic.com/pdfxobject) object. Use its canvas to define the watermark's appearance through drawing methods and properties. You can access the canvas through the [PdfXObject.Canvas](https://api.docotic.com/pdfxobject-canvas) property.

Use one of the [PdfCanvas.DrawXObject](https://api.docotic.com/pdfcanvas-drawxobject) methods to draw the watermark on a PDF page. You can reuse the same `PdfXObject` to apply the watermark to multiple pages.

To use the `PdfXObject` as a background instead of a watermark, set its [DrawOnBackground](https://api.docotic.com/pdfxobject-drawonbackground) property to `true`. Then use one of the `PdfCanvas.DrawXObject` methods to draw the background on one or more pages.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Watermarks & backgrounds](https://bitmiracle.com/pdf-library/edit/#watermarks)
* [Watermark PDFs using page background and foreground containers](https://bitmiracle.com/pdf-library/layout/compounds#watermarks)
