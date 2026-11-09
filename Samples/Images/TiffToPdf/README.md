# Convert TIFF to PDF in C# and VB.NET

This sample shows how to convert single-page and multi-page TIFF images to PDF using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use the [PdfDocument.OpenImage](https://api.docotic.com/pdfdocument-openimage) methods to open a TIFF image, whether it contains a single frame or multiple frames. You can then enumerate the image frames and add each one as a separate page to the PDF document.

The sample adjusts the PDF page size to match each image frame, so the image fills the entire page. Alternatively, you can keep the page size unchanged, scale each frame to fit the page, and center the resulting image on the page.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Combine images into PDF](https://bitmiracle.com/pdf-library/edit/#combine-images)
* [Recompress images before adding them to PDF in C# and VB.NET](/Samples/Images/AddRecompressedImages) sample