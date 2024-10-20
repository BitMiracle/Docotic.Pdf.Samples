# Mask images for PDF images in C# and VB.NET

This sample shows how to use mask images to make certain parts of PDF images transparent with the help of [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

You can add a transparent image to a PDF document using one of the [PdfDocument.AddImage](https://api.docotic.com/pdfdocument-addimage) methods that accept a mask image as a parameter. The mask image must have the same width and height as the image to be made transparent. Pixels of the mask image must use 1 bit per pixel.

To produce the output, PDF viewers will use the image and its mask image. For each position in the output, viewers will do this:
* Get the mask image pixel at the current position.
* If the mask image pixel is black, then get the pixel of the image at the same position and draw it.
* If the mask image pixel is white, draw nothing. 

Mask images encode black pixels using `0` bits. For white pixels, the mask images use `1` bits.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Create color-key mask for PDF image in C# and VB.NET](/Samples/Images/ColorMasks) sample
