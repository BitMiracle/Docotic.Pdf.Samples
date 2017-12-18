# Initial view
This sample shows how to create and use PdfDocument.InitialView property.

You can use InitialView property to setup a document view to be displayed by a PDF viewer application when document is opened. A document view specifies a page, its location and zoom level.

The InitialView property is null by default (which means that default view is to be used). You can create a document view (PdfDocumentView class) using PdfDocument.CreateView(..) method. After a document view is created, you can use methods and properties of PdfDocumentView class to setup that view.