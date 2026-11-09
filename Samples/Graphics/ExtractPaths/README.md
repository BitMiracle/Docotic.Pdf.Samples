# Extract paths from PDF documents in C# and VB.NET

This sample shows how to extract vector paths from a PDF page and copy them to a new document using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use one of the [PdfPage.GetObjects methods](https://api.docotic.com/pdfpage-getobjects) to retrieve the page objects. Enumerate the returned collection and identify objects of the [PdfPath](https://api.docotic.com/pdfpath) type to extract vector paths.

A path consists of one or more disconnected subpaths. Each subpath is a sequence of connected segments.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Vector graphics](https://bitmiracle.com/pdf-library/edit/#graphics)