# Setting canvas colors in C# and VB.NET

This sample shows how to use colors on a PDF page canvas.

## Description

[Docotic.Pdf library](https://bitmiracle.com/pdf-library/) supports Gray, RGB and CMYK device-dependent colors. Corresponding classes are [PdfGrayColor](https://api.docotic.com/pdfgraycolor), [PdfRgbColor](https://api.docotic.com/pdfrgbcolor) and [PdfCmykColor](https://api.docotic.com/pdfcmykcolor).

You can change the color of a [canvas pen](https://api.docotic.com/pdfcanvas-pen) to specify the color for strokes. Use the [PdfPen.Color property](https://api.docotic.com/pdfpen-color) for this. 

There is also a [brush](https://api.docotic.com/pdfcanvas-brush) that is used to fill areas and shapes. You can change the brush color using the [PdfBrush.Color property](https://api.docotic.com/pdfbrush-color).

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Using color profiles in C# and VB.NET](/Samples/Graphics/ColorProfiles) sample
* [Using semi-transparent colors in C# and VB.NET](/Samples/Graphics/Transparency) sample
* [Stroke and fill PDF canvas with patterns in C# and VB.NET](/Samples/Graphics/Patterns) sample