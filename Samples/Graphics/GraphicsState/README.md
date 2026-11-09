# Working with graphics state in C# and VB.NET

This sample shows how to save and restore the graphics state of a PDF canvas using the [PdfCanvas.SaveState](https://api.docotic.com/pdfcanvas-savestate) and [PdfCanvas.RestoreState](https://api.docotic.com/pdfcanvas-restorestate) methods.

Each [PDF canvas](https://api.docotic.com/pdfcanvas) maintains a set of properties that define its graphics state. These properties include the current pen, brush, font, text and graphics positions, and clipping area.

The [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) allows you to save the current graphics state and restore it later. Restoring a previously saved state is the only way to undo certain changes, such as applying a clipping area.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Set up clipping on a PDF canvas in C# and VB.NET](/Samples/Graphics/Clipping) sample