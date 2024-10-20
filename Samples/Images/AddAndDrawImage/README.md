# Add images to PDF in C# and VB.NET

This sample shows how to add images to your PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

There are several ways to add an image to your PDF document. You can create an image from a file in GIF/TIFF/PNG/BMP/JPEG formats. Besides files, the library can read images from streams and byte arrays. The library can create a PDF image from a `System.Drawing.Image` object, too.

To add a new image to a PDF document, use one of the [PdfDocument.AddImage](https://api.docotic.com/pdfdocument-addimage) methods. After that, use a [PdfCanvas.DrawImage](https://api.docotic.com/pdfcanvas-drawimage) method to draw the added image on a PDF canvas. With some of the `DrawImage` methods, you can specify the rotation angle and the size of the area to draw the image in.

You can draw the same image as many times as you want on different pages and with different output sizes and rotation angles.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add image to PDF](https://bitmiracle.com/pdf-library/edit/#add-image)