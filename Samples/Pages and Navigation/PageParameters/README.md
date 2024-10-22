# Set up PDF page in C# and VB.NET

This sample shows how to set up a page size, orientation, and rotation using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

You can specify the size of any page in pixels using the [PdfPage.Width](https://api.docotic.com/pdfpage-width) and [PdfPage.Height](https://api.docotic.com/pdfpage-height) properties. Or use [PdfPage.Size](https://api.docotic.com/pdfpage-size) property to specify the size using a value from the [PdfPaperSize](https://api.docotic.com/pdfpapersize) enumeration. This enumeration defines a lot of predefined paper formats like A4, Letter or Envelope.

To change the orientation of a page to portrait or landscape, use the [PdfPage.Orientation](https://api.docotic.com/pdfpage-orientation) property. You can specify a rotation for a page using the [PdfPage.Rotation](https://api.docotic.com/pdfpage-rotation) property. Rotation can be 90, 180, or 270 degrees in the clockwise direction.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Edit pages](https://bitmiracle.com/pdf-library/edit/#edit-pages)