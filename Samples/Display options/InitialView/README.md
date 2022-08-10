# Configure initial view for PDF documents
This sample shows how to set up an initial view for PDF document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use [PdfDocument.InitialView property](https://bitmiracle.com/pdf-library/help/pdfdocument.initialview.html)
to set up a document view to be displayed by a PDF viewer application when document is opened.
A document view specifies a page, its location and zoom level.

The InitialView property is null by default, which means that default view is to be used. You can create a document view
([PdfDocumentView class](https://bitmiracle.com/pdf-library/help/pdfdocumentview.html)) using [PdfDocument.CreateView methods](https://bitmiracle.com/pdf-library/help/pdfdocument.createview.html).
After a document view is created, you can use methods and properties of PdfDocumentView class to set up that view.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)