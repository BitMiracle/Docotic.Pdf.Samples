# Add hyperlink to a PDF page in C# and VB.NET

This sample shows how to add hyperlink to a page of your PDF document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description 

Use [PdfPage.AddHyperlink](https://api.docotic.com/pdfpage-addhyperlink) methods to add a link. The methods require bounds of the clickable area. You must also provide the URI to open when hyperlink is activated. 

These methods create a [PdfActionArea](https://api.docotic.com/pdfactionarea) on the page. The area has an action of [PdfUriAction](https://api.docotic.com/pdfuriaction) type attached. The action causes a URI to be resolved.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Use URI action in PDF documents](/Samples/Actions/UriAction)
* [Add link to a PDF page in C# and VB.NET](/Samples/Pages%20and%20Navigation/LinkToPage)