# Extract PDF image coordinates in C# and VB.NET

This sample shows how to extract images from a PDF document with their location using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

Use the [PdfPage.GetPaintedImages](https://api.docotic.com/pdfpage-getpaintedimages) method to get the collection of all images drawn on the page. The collection contains instances of the [PdfPaintedImage](https://api.docotic.com/pdfpaintedimage) class. 

Using the properties of the `PdfPaintedImage` class, you can access the [PdfImage](https://api.docotic.com/pdfimage) object for the painted image and the bounds of the image on the page.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Extract painted images from PDF using C# and VB.NET](/Samples/Images/ExtractPaintedImages) sample
* [Extract images from PDF in C# and VB.NET](/Samples/Images/ExtractImages) sample