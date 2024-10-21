# Use different fonts in PDF documents with Docotic.Pdf.Layout add-on
This sample shows how to manage fonts in PDF documents using [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/).

Use [Document.TextStyleWithFont methods](https://api.docotic.com/layout/document-textstylewithfont)
to create a text style based on a font from the system font collection, a file or a stream. You can also set a desired font embed style.

You can provide a font loader using the [PdfDocumentBuilder.FontLoader method](https://api.docotic.com/layout/pdfdocumentbuilder-fontloader)
to override the system font collection.

The library provides 2 ways to handle missing font glyphs. You can set a common handler using the
[PdfDocumentBuilder.MissingGlyphHandler](https://api.docotic.com/layout/pdfdocumentbuilder-missingglyphhandler).
Or you can apply a fallback text style using the [TextStyle.Fallback method](https://api.docotic.com/layout/textstyle-fallback).

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [C# PDF generation quick start guide](https://bitmiracle.com/pdf-library/layout/getting-started)
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf)
* [Text styling](/Samples/Layout/TextStyling)
* [Typography](/Samples/Layout/Typography)