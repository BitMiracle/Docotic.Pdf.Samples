# Create and read named destinations in PDF documents
This sample shows how to work with named destinations in PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

In Docotic.Pdf, destinations are called `views`. The [PdfDocumentView class](https://api.docotic.com/pdfdocumentview)
provides an API for PDF destinations. Use [PdfDocument.CreateView methods](https://api.docotic.com/pdfdocument-createview)
to create a view.

The [PdfDocument.SharedViews](https://api.docotic.com/pdfdocument-sharedviews) property provides access to the collection of all named views in a document. The code in this sample first reads existing named views and then creates a new named view.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Use GoTo actions in PDF documents](/Samples/Actions/GoToAction) code sample
* [Links](https://bitmiracle.com/pdf-library/edit/#link-annotations)