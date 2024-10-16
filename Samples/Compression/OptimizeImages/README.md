# Optimize PDF images in C# and VB.NET
This sample shows how to optimize existing images in PDF document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

Use one of [PdfImage.RecompressWith* methods](https://api.docotic.com/pdfimage) to optimize images in PDF document. Recompression of images is an effective way to greatly decrease size of the output PDF file.

You can also resize images in a PDF document. To do so please use one of the [PdfImage.ResizeTo](https://api.docotic.com/pdfimage-resizeto) or [PdfImage.Scale](https://api.docotic.com/pdfimage-scale) methods.

You can recompress images using Flate, Jpeg and CCITT compression schemes. Please note that recompression is often a lossy process.

This sample shows only one approach to reduce size of a PDF. Please check [Compress PDF documents in C# and VB.NET](/Samples/Compression/CompressAllTechniques) sample code to see more approaches.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Compress PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/optimize/compress)
* [Change compression scheme](https://bitmiracle.com/pdf-library/edit/#recompress-images)
* [Resize images](https://bitmiracle.com/pdf-library/edit/#resize-images)