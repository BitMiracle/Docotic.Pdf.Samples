# Stroke and fill PDF canvas with patterns in C# and VB.NET

This sample shows how to use colored and uncolored tiling patterns on a PDF canvas. The code uses [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) API.

## Description

A tiling pattern consists of a small graphical figure called a pattern cell. Painting with the pattern replicates the cell at fixed horizontal and vertical intervals to fill an area. A colored pattern specifies the colors used to paint the pattern cell. An uncolored pattern does not specify any color information. Instead, the entire pattern cell is painted with a separately specified color each time the pattern is used.

The [PdfTilingPattern](https://api.docotic.com/pdftilingpattern) class is used for both kinds of tiling patterns. You can add a pattern to your document using the [PdfDocument.AddColoredPattern](https://api.docotic.com/pdfdocument-addcoloredpattern) or the [PdfDocument.AddUncoloredPattern](https://api.docotic.com/pdfdocument-adduncoloredpattern) method.

For any pattern added to a document, construct its appearance by drawing on the pattern canvas. When done constructing the pattern, use it to stroke lines and shapes and/or to fill areas on a canvas. Use the [Pattern property of a pen](https://api.docotic.com/pdfpen-pattern) to set up the pattern for stroking lines and shapes. Set the pattern object to the [Pattern property of a brush](https://api.docotic.com/pdfbrush-pattern) to use the pattern for filling areas.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Setting canvas colors in C# and VB.NET](/Samples/Graphics/Colors) sample
* [Using color profiles in C# and VB.NET](/Samples/Graphics/ColorProfiles) sample