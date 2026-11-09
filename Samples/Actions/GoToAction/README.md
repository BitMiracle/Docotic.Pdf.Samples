# Use GoTo actions in PDF documents
This sample shows how to use Go-To actions in PDF documents with the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

A Go-To (or jump) action navigates to a specified page in a PDF document. You can create one using the [PdfDocument.CreateGoToPageAction methods](https://api.docotic.com/pdfdocument-creategotopageaction). These methods return an instance of the [PdfGoToAction class](https://api.docotic.com/pdfgotoaction).

Use the properties and methods of `PdfGoToAction` to specify the target page and its position in the document window. The `PdfGoToAction.Offset` property determines which point on the target page is positioned at the upper-left corner of the window.

You can also use Go-To actions and several other action types as widget event handlers. For example, see the [PdfButton.OnMouseDown property](https://api.docotic.com/pdfcontrol-onmousedown).

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add annotations, actions and scripts to PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/annotations-and-actions)