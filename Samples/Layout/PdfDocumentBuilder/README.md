# PDF document builder in Docotic.Pdf.Layout add-on
This sample shows how to use the `PdfDocumentBuilder` class from [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/).

The [PdfDocumentBuilder](https://api.docotic.com/layout/pdfdocumentbuilder) is an entry point
in Docotic.Pdf.Layout add-on. It allows you to set parameters of generated PDF documents:
* encryption
* metadata
* PDF version
* linearization (fast web view)

The `PdfDocumentBuilder` also provides methods for configuring document generation flow. For example, you can provide a custom font loader,
stream provider, or a handler for missing glyphs.

You can also use features of the core Docotic.Pdf library. I mean, generate a PDF document using the `PdfDocumentBuilder` and then pass the resulting
document to the [PdfDocument](https://api.docotic.com/pdfdocument) class.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [C# PDF generation quick start guide](https://bitmiracle.com/pdf-library/layout/getting-started)
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf)
* [Page layout in PDF documents](/Samples/Layout/Pages)