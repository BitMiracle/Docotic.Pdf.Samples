# Create and read named destinations in PDF documents
This sample shows how to work with named destinations in PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

In Docotic.Pdf, destinations are called `views`. The [PdfDocumentView class](https://bitmiracle.com/pdf-library/api/pdfdocumentview)
provides API for PDF destinations. Use [PdfDocument.CreateView methods](https://bitmiracle.com/pdf-library/api/pdfdocument-createview)
to create a view.

Named views are stored in the [PdfDocument.SharedViews collection](https://bitmiracle.com/pdf-library/api/pdfdocument-sharedviews).
In this sample, we read existing named views and then create a new named view.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add bookmarks and links to PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/bookmarks-and-links) article
* [Use GoTo actions in PDF documents](/Samples/Actions/GoToAction) code sample