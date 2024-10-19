# Working with graphics state in C# and VB.NET

This sample shows how to save and restore graphics state using [PdfCanvas.SaveState](https://api.docotic.com/pdfcanvas-savestate) and [PdfCanvas.RestoreState](https://api.docotic.com/pdfcanvas-restorestate) methods.

## Description

Every [PDF canvas](https://api.docotic.com/pdfcanvas) maintains a set of properties, such as pen, brush, current positions for text and graphics, font, e.t.c. This set of properties is called graphics state.

Using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) you can save the current graphics state. Later, you can restore the previously saved state. Restoring the saved state is the only way to remove clipping, for example.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Set up clipping on a PDF canvas in C# and VB.NET](/Samples/Graphics/Clipping) sample