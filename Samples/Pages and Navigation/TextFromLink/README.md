# Extract text from link’s target in C# and VB.NET

This sample shows how to retrieve a link information and extract text starting from the where the link points to.

## Description

[Docotic.Pdf library](https://bitmiracle.com/pdf-library/) represents PDF page links as [PdfActionArea](https://api.docotic.com/pdfactionarea) objects. By examining a `PdfActionArea` object, you can get a page which hosts the action area and the area bounds. 

There is also the [PdfActionArea.Action](https://api.docotic.com/pdfactionarea-action) property. For links, this property contains an instance of the [PdfGoToAction](https://api.docotic.com/pdfgotoaction) class. Any `PdfGoToAction` object contains information about the page associated with the link.

To extract text from the link’s target page, get the top offset of the target position. Then retrieve all text of the target page placed below the top offset.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Extract text from PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/pdf-text/extract)