# Construct graphics paths in C# and VB.NET

This sample shows how to construct and use graphics paths on PDF canvas using Docotic.Pdf library.

## Description 

Graphics path is a series of connected lines and curves. You can construct a graphics path using methods of the [PdfCanvas](https://api.docotic.com/pdfcanvas) class with names that start with `Append` (e.g. `AppendLineTo`, `AppendRectangle`). All those methods do not place any marks on a canvas. Instead, they add corresponding shapes to the current graphics path.

You can fill, stroke, or reset the constructed graphics path. You can also use the constructed path as a clip region of a canvas.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Working with graphics state in C# and VB.NET](/Samples/Graphics/GraphicsState) sample
* [Set up clipping on a PDF canvas in C# and VB.NET](/Samples/Graphics/Clipping) sample