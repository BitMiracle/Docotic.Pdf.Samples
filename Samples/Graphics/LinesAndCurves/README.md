# Draw lines and curves in C# and VB.NET

This sample shows how to draw straight lines and cubic Bezier curves on PDF canvas using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

To draw anything in a PDF document, use the methods and properties of the [PdfCanvas](https://api.docotic.com/pdfcanvas) class. You can access a canvas using the `Canvas` property of [PdfPage](https://api.docotic.com/pdfpage), [PdfXObject](https://api.docotic.com/pdfxobject), or [PdfTilingPattern](https://api.docotic.com/pdftilingpattern) classes.

There are plenty of methods that draw lines, curves and shapes. All these methods have names that start with `Draw` (e.g. `DrawLineTo` method). The [current pen](https://api.docotic.com/pdfcanvas-pen) is used to draw all kinds of lines.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Setting canvas colors in C# and VB.NET](/Samples/Graphics/Colors) sample