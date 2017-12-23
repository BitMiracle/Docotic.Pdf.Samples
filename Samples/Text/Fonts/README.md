# Fonts
This sample shows how to use fonts in Docotic.Pdf library.

Docotic.Pdf library provides several ways to add fonts to PDF document. You can add a font from a file using PdfDocument.AddFontFromFile(..) method. This method supports TrueType fonts, TrueType font collections and Type1 fonts.

You can also add any font installed on your system, a one of 14 built-in fonts or a font created from a System.Drawing.Font object using PdfDocument.AddFont(..) method. The names of 14 built-in PDF fonts are in PdfBuiltInFont enumeration. 

You can specify a font to use on canvas using PdfCanvas.Font property. PdfCanvas.FontSize property specifies the size of a font.