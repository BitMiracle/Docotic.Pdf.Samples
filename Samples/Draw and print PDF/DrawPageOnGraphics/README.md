# Draw PDF page on Graphics in C# and VB.NET
This sample shows how to draw a PDF page on the drawing surface of a [System.Drawing.Graphics](https://docs.microsoft.com/dotnet/api/system.drawing.graphics) using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Note that the resolution of a [PdfPage](https://bitmiracle.com/pdf-library/help/pdfpage.html) is usually less than the resolution of the drawing surface of a Graphics.

For PdfPage, the resolution is usually equal to 72 DPI. The resolution of a Graphics depends on the resolution of its underlying device context or image. I.e., for Graphics objects based on an image or a screen context, the resolution is usually equal to 96 DPI. For Graphics objects based on a printer context, the resolution is usually even bigger (e.g., 300 or 600 DPI).

It means that if you create Graphics based on a System.Drawing.Bitmap, the size of the bitmap should be scaled. The scale factor is the quotient of bitmap resolution and PdfPage resolution. 

This example shows how to create Graphics based on the bitmap with a resolution equal to 300 DPI and draw a PdfPage on the drawing surface of that Graphics.

## See also
* [Render and print PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/draw-print-pdf.aspx) article
* [Convert PDF to image in C# and VB.NET](https://bitmiracle.com/pdf-library/convert-pdf-to-image.aspx) article
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)