# Add header and footer to PDF using Canvas API in C# and VB.NET

This sample shows how to add common header and footer to a PDF document using Canvas API provided by [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

If you create the PDF from scratch, consider using the API for generating PDFs. It might be simpler to [add header and footer using the Layout API](https://bitmiracle.com/pdf-library/layout/getting-started#header-footer).

## Description

You can add text, images, vector graphics to the header and footer of a PDF page. It’s important to calculate position and orientation of the content properly, taking possible rotation of a PDF page into account.

Use the [PdfPage.Rotation](https://api.docotic.com/pdfpage-rotation) property to transform the page’s coordinate space before drawing the content. This sample shows how to add centered text to the header and right-aligned page numbers to the footer. You can use the same technique for any type of content and positioning.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add header and footer to PDF documents in C# and VB.NET](/Samples/Layout/HeaderFooter)