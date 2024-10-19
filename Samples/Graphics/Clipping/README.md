# Set up clipping on a PDF canvas in C# and VB.NET

This sample shows how to use the current graphics path as a clip region of a canvas.

## Description

[Docotic.Pdf library](https://bitmiracle.com/pdf-library/) provides methods to add vector graphics like lines, curves, and shapes to [graphics path](/Samples/Graphics/Paths). You can set the current graphics path as a clip region of a canvas using the [PdfCanvas.SetClip](https://api.docotic.com/pdfcanvas-setclip) method. 

Once you set a clip region, you can fill or stroke it. You can also just continue drawing on a canvas. When a canvas has a clip region, any part of a graphics, text and images that lay outside the clip region will not be visible.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Construct graphics paths in C# and VB.NET](/Samples/Graphics/Paths) sample