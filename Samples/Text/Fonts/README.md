# Using PDF fonts in C# and VB.NET

This sample shows how to use PDF fonts in [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

The code uses Canvas API that can work with fonts in new and existing PDF documents. If you are going to create a new PDF, then consider using Layout API that can [make PDF generation easier](https://bitmiracle.com/pdf-library/layout/).

## Description

The library provides several ways to add fonts to PDF documents. You can add a font from a file using one of the [PdfDocument.AddFontFromFile](https://api.docotic.com/pdfdocument-addfontfromfile) methods. This method supports TrueType fonts, TrueType font collections, and Type1 fonts.

[PdfDocument.AddFont](https://api.docotic.com/pdfdocument-addfont) methods can add any font installed on your system, a one of 14 built-in fonts or a font created from a `System.Drawing.Font` object. The names of 14 built-in PDF fonts are in the [PdfBuiltInFont](https://api.docotic.com/pdfbuiltinfont) enumeration.

You can specify a font to use for a canvas text by using the [PdfCanvas.Font](https://api.docotic.com/pdfcanvas-font) property. The [PdfCanvas.FontSize](https://api.docotic.com/pdfcanvas-fontsize) property specifies the size of the font.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Unicode text in PDF using C# and VB.NET](/Samples/Text/Internationalization)