# Draw text on PDF canvas in C# and VB.NET

This sample shows how to draw text on a canvas using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

The code uses Canvas API that can add text to any PDF document. If you are going to create a new PDF, then consider using Layout API that can [make PDF generation easier](https://bitmiracle.com/pdf-library/layout/).

## Description

To draw text on a canvas, use the [PdfCanvas.DrawString](https://api.docotic.com/pdfcanvas-drawstring) or [PdfCanvas.DrawText](https://api.docotic.com/pdfcanvas-drawtext) methods. These methods draw text using the [current canvas font](https://api.docotic.com/pdfcanvas-font).

`DrawString` methods draw a single line of text either from the [current text position](https://api.docotic.com/pdfcanvas-textposition) or inscribed in the specified rectangle. The `DrawText` method can draw multiple lines of text.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add new text](https://bitmiracle.com/pdf-library/edit/#add-text)
