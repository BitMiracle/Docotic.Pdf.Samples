# Convert PDF to bitonal TIFF in C# and VB.NET
This sample shows how to save a PDF page or the whole PDF document as a bitonal (black and white) TIFF image using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

To produce a bitonal TIFF, create drawing options with specific image compression options. After that, use that drawing options with [PdfDocument.SaveAsTiff](https://bitmiracle.com/pdf-library/api/pdfdocument-saveastiff) or [PdfPage.Save](https://bitmiracle.com/pdf-library/api/pdfpage-save) methods.

To create [PdfDrawOptions](https://bitmiracle.com/pdf-library/api/pdfdrawoptions), use [PdfDrawOptions.Create() method](https://bitmiracle.com/pdf-library/api/pdfdrawoptions-create).
After that, use [TiffImageCompressionOptions.SetBitonal()](https://bitmiracle.com/pdf-library/api/tiffimagecompressionoptions-setbitonal) and [ImageCompressionOptions.CreateTiff()](https://bitmiracle.com/pdf-library/api/imagecompressionoptions-createtiff) methods to create compression options for bitonal TIFF output.

## See also
* [Convert PDF to image in C# and VB.NET](https://bitmiracle.com/pdf-library/pdf-image/convert) article
* [Render and print PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/draw-print-pdf) article
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)