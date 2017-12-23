# Patterns
This sample shows how to use colored and uncolored tiling patterns.

A tiling pattern consists of a small graphical figure called a pattern cell. Painting with the pattern replicates the cell at fixed horizontal and vertical intervals to fill an area. A colored pattern specifies the colors used to paint the pattern cell. An uncolored pattern does not specify any color information. Instead, the entire pattern cell is painted with a separately specified color each time the pattern is used.

PdfTilingPattern class is used for both kinds of tiling patterns. You can add a pattern to your document using PdfDocument.AddColoredPattern or PdfDocument.AddUncoloredPattern method.

After a pattern is added to your document you should construct its appearance by drawing on a pattern canvas. When pattern is constructed, you can use it to stroke lines and shapes and/or to fill areas on a canvas. Use PdfCanvas.Pen.Pattern property to stroke lines and shapes or PdfCanvas.Brush.Pattern property to fill areas with a pattern.