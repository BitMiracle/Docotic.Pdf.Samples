# PDF document builder in Docotic.Pdf.Layout add-on
This sample shows how to use PdfDocumentBuilder class from [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/).

[PdfDocumentBuilder](https://bitmiracle.com/pdf-library/help/pdfdocumentbuilder.html) is an entry point
in Docotic.Pdf.Layout add-on. It allows you to set parameters of generated PDF documents:
* encryption
* metadata
* PDF version
* linearization (fast web view)

`PdfDocumentBuilder` also provides methods for configuring document generation flow. For example, you can provide a custom font loader,
stream provider, or a handler for missing glyphs.

You can also use features of the core Docotic.Pdf library. I.e., generate PDF document using `PdfDocumentBuilder` and then pass the resulting
document to the PdfDocument class.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf.aspx) article
* [Page layout in PDF documents](/Samples/Layout/Pages)
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)