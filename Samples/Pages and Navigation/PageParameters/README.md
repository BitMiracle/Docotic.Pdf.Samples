# Set up PDF pages in C# and VB.NET

This sample shows how to configure PDF page size, orientation, and rotation using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

You can specify a page's width and height in default user space units using the [PdfPage.Width](https://api.docotic.com/pdfpage-width) and [PdfPage.Height](https://api.docotic.com/pdfpage-height) properties. One default user space unit corresponds to one pixel at the default resolution. Alternatively, use the [PdfPage.Size](https://api.docotic.com/pdfpage-size) property to select a predefined paper size from the [PdfPaperSize](https://api.docotic.com/pdfpapersize) enumeration. The enumeration includes common paper formats such as A4, Letter, and Envelope.

To set a page to portrait or landscape orientation, use the [PdfPage.Orientation](https://api.docotic.com/pdfpage-orientation) property. To rotate a page, use the [PdfPage.Rotation](https://api.docotic.com/pdfpage-rotation) property. Supported rotation values are 90, 180, and 270 degrees clockwise.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Edit pages](https://bitmiracle.com/pdf-library/edit/#edit-pages)