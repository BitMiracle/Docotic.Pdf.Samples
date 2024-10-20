# Convert TIFF to PDF in C# and VB.NET

This sample shows how to create a PDF from single- and multi-page TIFF images using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

Use [PdfDocument.OpenImage](https://api.docotic.com/pdfdocument-openimage) methods to open a potentially multipage image. After that, you can enumerate all frames (pages) in the image and add them to the PDF.

The code adjusts the page size, so the image occupies the whole page. As an alternative, you can keep the page size unchanged and instead scale the frame up or down. Then you can place the scaled image in the center of the page.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Combine images into PDF](https://bitmiracle.com/pdf-library/edit/#combine-images)
* [Recompress images before adding them to PDF in C# and VB.NET](/Samples/Images/AddRecompressedImages) sample