# Extract paths from PDF in C# and VB.NET

This sample shows how to extract vector paths from a page and copy them to a new document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

Use one of the [PdfPage.GetObjects](https://api.docotic.com/pdfpage-getobjects) methods to get the collection of page objects. Then enumerate the collection and use objects of the [PdfPath type](https://api.docotic.com/pdfpath) to extract vector paths.

A path is a one or more disconnected sub-paths. Each sub-path comprises a sequence of connected segments.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Vector graphics](https://bitmiracle.com/pdf-library/edit/#graphics)