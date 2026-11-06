# Remove XObjects from PDF in C# and VB.NET

This sample shows how to remove a watermark from PDF pages when the watermark is represented by an XObject.

The [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) provides access to a page's XObjects through the [PdfPage.XObjects](https://api.docotic.com/pdfpage-xobjects) property, which returns a `PdfXObjectMap` object.

Use the `PdfXObjectMap.Remove` method to remove an individual XObject. To remove all XObjects from a page, use the `PdfXObjectMap.Clear` method.

Note that XObjects are not necessarily watermarks. They can also represent backgrounds, logos, images, and other reusable content in a PDF document.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)