# Extract PDF image coordinates in C# and VB.NET

This sample shows how to extract images from a PDF document and obtain their locations on the page using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use the [PdfPage.GetPaintedImages](https://api.docotic.com/pdfpage-getpaintedimages) method to get a collection of all images drawn on a page. Each item in the collection is an instance of the [PdfPaintedImage](https://api.docotic.com/pdfpaintedimage) class.

The `PdfPaintedImage` class provides access to the underlying [PdfImage](https://api.docotic.com/pdfimage) object and the bounds of the image on the page.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Extract painted images from PDF using C# and VB.NET](/Samples/Images/ExtractPaintedImages) sample
* [Extract images from PDF in C# and VB.NET](/Samples/Images/ExtractImages) sample