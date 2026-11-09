# Optimize PDF images in C# and VB.NET
This sample shows how to optimize existing images in PDF document using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use one of the [PdfImage.RecompressWith* methods](https://api.docotic.com/pdfimage) to recompress images in a PDF document. Image recompression can significantly reduce the size of the output PDF file.

You can also resize images in a PDF document using the [PdfImage.ResizeTo](https://api.docotic.com/pdfimage-resizeto) or [PdfImage.Scale](https://api.docotic.com/pdfimage-scale) methods.

Docotic.Pdf supports image recompression using the Flate, JPEG, and CCITT compression schemes. Keep in mind that recompression may result in a loss of image quality, depending on the selected compression scheme and settings.

This sample demonstrates just one way to reduce PDF file size. For other optimization techniques, see the [Compress PDF documents in C# and VB.NET](/Samples/Compression/CompressAllTechniques) sample.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Compress PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/optimize/compress)
* [Change compression scheme](https://bitmiracle.com/pdf-library/edit/#recompress-images)
* [Resize images](https://bitmiracle.com/pdf-library/edit/#resize-images)