# Add watermarks and backgrounds to PDF using C# or VB.NET
This sample shows how to apply PDF watermarks and backgrounds in C# or VB.NET application.

Use the `PdfDocument.CreateXObject` method to add a `PdfXObject` object to your document. Use the new object for a watermark. You can set up the PDF watermark appearance using methods and properties of the object’s canvas. To access the canvas, use the `PdfXObject.Canvas` property.

The `PdfCanvas.DrawXObject` methods apply the watermark to a page. It’s ok to apply the same watermark to more than one page.

To convert a watermark to a background, set the `DrawOnBackground` property of the created object to `true`. After that, use apply the background to one or more pages using one of the PdfCanvas.DrawXObject methods.

## See also
* [Watermark PDFs using page background and foreground containers](https://bitmiracle.com/pdf-library/layout/compounds#watermarks)
