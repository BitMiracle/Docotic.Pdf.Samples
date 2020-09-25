# Convert PDF to bitonal TIFF in C# and VB.NET
This sample shows how to save a PDF page or the whole PDF document as a bitonal (black and white) TIFF image using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

To produce a bitonal TIFF, create drawing options with specific image compression options. After that, use that drawing options with [PdfDocument.SaveAsTiff](https://bitmiracle.com/pdf-library/help/pdfdocument.saveastiff.html) or [PdfPage.Save](https://bitmiracle.com/pdf-library/help/pdfpage.save.html) methods.

To create [PdfDrawOptions](https://bitmiracle.com/pdf-library/help/pdfdrawoptions.html), use [PdfDrawOptions.Create() method](https://bitmiracle.com/pdf-library/help/pdfdrawoptions.create.html). After that use [ImageCompressionOptions.CreateBitonalTiff()](https://bitmiracle.com/pdf-library/help/imagecompressionoptions.createbitonaltiff.html) to create compression options for bitonal TIFF output.

## See also
* [Convert PDF to image in C# and VB.NET](https://bitmiracle.com/pdf-library/convert-pdf-to-image.aspx) article
* [Render and print PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/draw-print-pdf.aspx) article
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)