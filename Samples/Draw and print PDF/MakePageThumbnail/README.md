# Make PDF page thumbnail in C# and VB.NET
This sample shows how to make a thumbnail of a PDF page in .NET using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use [PdfDrawOptions.CreateFitSize method](https://bitmiracle.com/pdf-library/help/pdfdrawoptions.createfitsize.html) to set up drawing options. It will cause the page to be drawn as an image of the specified size. Then pass created [PdfDrawOptions](https://bitmiracle.com/pdf-library/help/pdfdrawoptions.html) to [PdfPage.Save method](https://bitmiracle.com/pdf-library/help/pdfpage.save.html) to save the thumbnail of the page to a stream or a file.

There are also [PdfDrawOptions.CreateFitWidth](https://bitmiracle.com/pdf-library/help/pdfdrawoptions.createfitwidth.html) and [PdfDrawOptions.CreateFitHeight](https://bitmiracle.com/pdf-library/help/pdfdrawoptions.createfitheight.html) methods. These methods are useful when you need thumbnail images of specified width or height and have no requirements for the second dimension of thumbnail images.

## See also
* [Convert PDF to image in C# and VB.NET](https://bitmiracle.com/pdf-library/convert-pdf-to-image.aspx) article
* [Render and print PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/draw-print-pdf.aspx) article
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)