# Use different fonts in PDF documents with Docotic.Pdf.Layout add-on
This sample shows how to manage fonts in PDF documents using [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/).

Use Document.TextStyleWithFont methods to create a text style based on a font from the system font collection,
a file or a stream. You can also set a desired font embed style.

You can provide a font loader using PdfDocumentBuilder.FontLoader method to override the system font collection.

The library provides 2 ways to handle missing font glyphs. You can set a common handler using PdfDocumentBuilder.MissingGlyphHandler.
Or you can apply a fallback text style using TextStyle.Fallback method.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Text styling](/Samples/Layout/TextStyling)
* [Typography](/Samples/Layout/Typography)
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)