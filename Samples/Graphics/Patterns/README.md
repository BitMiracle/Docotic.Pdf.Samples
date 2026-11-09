# Stroke and fill PDF canvases with patterns in C# and VB.NET

This sample shows how to use colored and uncolored tiling patterns on a PDF canvas with the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

A tiling pattern consists of a small graphical figure called a pattern cell. When a pattern is used for painting, the PDF renderer repeats the pattern cell at fixed horizontal and vertical intervals to cover an area.

A colored pattern defines the colors used to paint its cells. An uncolored pattern does not define colors. Instead, you specify a separate color that is applied to the entire pattern cell each time the pattern is used.

The [PdfTilingPattern](https://api.docotic.com/pdftilingpattern) class represents both types of tiling patterns. Use the [PdfDocument.CreateColoredPattern](https://api.docotic.com/pdfdocument-createcoloredpattern) or [PdfDocument.CreateUncoloredPattern](https://api.docotic.com/pdfdocument-createuncoloredpattern) method to create a pattern.

After creating a pattern, define its appearance by drawing on its canvas. You can then use the pattern to stroke lines and shapes or fill areas on a PDF canvas.

To use a pattern for stroking, assign it to the [Pattern property of a pen](https://api.docotic.com/pdfpen-pattern). To use a pattern for filling, assign it to the [Pattern property of a brush](https://api.docotic.com/pdfbrush-pattern).

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Setting canvas colors in C# and VB.NET](/Samples/Graphics/Colors) sample
* [Using color profiles in C# and VB.NET](/Samples/Graphics/ColorProfiles) sample