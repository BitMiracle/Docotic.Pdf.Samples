# Extract text from a link's target in C# and VB.NET

This sample shows how to retrieve information about a PDF link and extract text starting from the position to which the link points.

The [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) represents PDF page links as [PdfActionArea](https://api.docotic.com/pdfactionarea) objects. By examining a `PdfActionArea` object, you can identify the page containing the link and retrieve the bounds of the clickable area.

The [PdfActionArea.Action](https://api.docotic.com/pdfactionarea-action) property provides access to the action associated with the link. For internal links, this property contains an instance of the [PdfGoToAction](https://api.docotic.com/pdfgotoaction) class. A `PdfGoToAction` object provides information about the target page and position.

To extract text from the link's target page, first retrieve the top coordinate of the target position (see the description of the [PdfDocumentView.Top](https://api.docotic.com/pdfdocumentview-top) property). Then extract text from the target page starting at that position. Note that the top coordinate is measured relative to the bottom edge of the page.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Extract text from PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/pdf-text/extract)