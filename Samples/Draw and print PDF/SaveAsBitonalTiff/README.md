# Convert PDF to bitonal TIFF in C# and VB.NET
This sample shows how to save a PDF page or the whole PDF document as a bitonal (black and white) TIFF image using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

To produce a bitonal TIFF, create drawing options with specific image compression options. After that, use that drawing options with [PdfDocument.SaveAsTiff](https://api.docotic.com/pdfdocument-saveastiff) or [PdfPage.Save](https://api.docotic.com/pdfpage-save) methods.

To create [PdfDrawOptions](https://api.docotic.com/pdfdrawoptions), use [PdfDrawOptions.Create method](https://api.docotic.com/pdfdrawoptions-create).
After that, use [TiffImageCompressionOptions.SetBitonal](https://api.docotic.com/tiffimagecompressionoptions-setbitonal) and [ImageCompressionOptions.CreateTiff](https://api.docotic.com/imagecompressionoptions-createtiff) methods to create compression options for bitonal TIFF output.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Convert PDF to image in C# and VB.NET](https://bitmiracle.com/pdf-library/pdf-image/convert)
* [Render and print PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/draw-print-pdf)