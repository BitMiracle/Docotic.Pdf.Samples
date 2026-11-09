# Add headers and footers to PDF using the Canvas API in C# and VB.NET

This sample shows how to add a common header and footer to PDF pages using the Canvas API provided by the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

If you are creating a PDF from scratch, consider using the PDF generation API instead. It may be simpler to [add headers and footers using the Layout API](https://bitmiracle.com/pdf-library/layout/#header-footer).

You can add text, images, and vector graphics to PDF page headers and footers. When positioning this content, account for the page's rotation to ensure that the content appears in the correct position and orientation.

Use the [PdfPage.Rotation](https://api.docotic.com/pdfpage-rotation) property to adjust the page's coordinate system before drawing. This sample demonstrates how to add centered text to the header and right-aligned page numbers to the footer. You can apply the same technique to other types of content and positioning.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add header and footer to PDF documents in C# and VB.NET](/Samples/Layout/HeaderFooter)