# Add link to a PDF page in C# and VB.NET

This sample shows how to add a link from one page of your PDF to another using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

Use [PdfPage.AddLinkToPage](https://api.docotic.com/pdfpage-addlinktopage) methods to add a link. The methods require bounds of the clickable area. In addition, you must specify the target page. PDF viewers will open the page when the link is activated. 

These methods create a [PdfActionArea](https://api.docotic.com/pdfactionarea) on the page. The area has an action of [PdfGoToAction](https://api.docotic.com/pdfgotoaction) type attached. The action changes the current page to the specified one.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Use GoTo actions in PDF documents](/Samples/Actions/GoToAction)
* [Add hyperlink to a PDF page in C# and VB.NET](/Samples/Pages%20and%20Navigation/Hyperlink)
* [Links](https://bitmiracle.com/pdf-library/edit/#link-annotations)