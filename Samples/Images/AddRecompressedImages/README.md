# Recompress images before adding them to PDF in C# and VB.NET

This sample shows how to change the compression scheme of an image while adding it to a PDF document.

## Description 

To recompress an image or a frame of an image before adding it to a PDF document:
* Open the image using the [PdfDocument.OpenImage](https://api.docotic.com/pdfdocument-openimage) method.
* Setup compression options for all or selected frames. 

Depending on the options, [the library](https://bitmiracle.com/pdf-library/) will decompress some frames and then compress them again before adding them to the document.

You can compress images using Flate, Jpeg and CCITT compression schemes. It is possible to decompress an image completely, too.

The library can also [repack the images, already added to the document](/Samples/Compression/OptimizeImages).

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
