# Unicode text in PDF using C# and VB.NET

This sample shows how to draw text in different languages using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

The code uses Canvas API that can add text to any PDF document. If you are going to create a new PDF, then consider using Layout API that can [make PDF generation easier](https://bitmiracle.com/pdf-library/layout/).

## Description

The library can draw text in any language, but the current canvas font must support all characters in the text.

One use set up a font that [contains glyphs for all characters](https://api.docotic.com/pdffont-containsglyphsfortext) in the text, you can draw the text using [PdfCanvas.DrawString](https://api.docotic.com/pdfcanvas-drawstring) and [PdfCanvas.DrawText](https://api.docotic.com/pdfcanvas-drawtext) methods.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Draw text on PDF canvas in C# and VB.NET](/Samples/Text/DrawText)