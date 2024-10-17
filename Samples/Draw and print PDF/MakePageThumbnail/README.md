# Make PDF page thumbnail in C# and VB.NET
This sample shows how to make a thumbnail of a PDF page in .NET using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use [PdfDrawOptions.CreateFitSize method](https://api.docotic.com/pdfdrawoptions-createfitsize) to set up drawing options. It will cause the page to be drawn as an image of the specified size. Then pass created [PdfDrawOptions](https://api.docotic.com/pdfdrawoptions) to [PdfPage.Save method](https://api.docotic.com/pdfpage-save) to save the thumbnail of the page to a stream or a file.

There are also [PdfDrawOptions.CreateFitWidth](https://api.docotic.com/pdfdrawoptions-createfitwidth) and [PdfDrawOptions.CreateFitHeight](https://api.docotic.com/pdfdrawoptions-createfitheight) methods. These methods are useful when you need thumbnail images of specified width or height and have no requirements for the second dimension of thumbnail images.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Convert PDF to image in C# and VB.NET](https://bitmiracle.com/pdf-library/pdf-image/convert)
* [Render and print PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/draw-print-pdf)