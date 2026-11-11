# Add a link to a PDF page in C# and VB.NET

This sample shows how to add an internal link that navigates from one PDF page to another using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use the `PdfPage.AddLinkToPage` method to add a link. Specify the bounds of the clickable area and the target page. When a user activates the link, the PDF viewer navigates to the target page.

These methods create a [PdfActionArea](https://api.docotic.com/pdfactionarea) on the source page and attach a [PdfGoToAction](https://api.docotic.com/pdfgotoaction) to it. The action instructs the PDF viewer to navigate to the specified page.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Use GoTo actions in PDF documents](/Samples/Actions/GoToAction)
* [Add hyperlink to a PDF page in C# and VB.NET](/Samples/Pages%20and%20Navigation/Hyperlink)
* [Links](https://bitmiracle.com/pdf-library/edit/#link-annotations)