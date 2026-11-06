# List attachments and file annotation names in PDF document

This sample shows how to list the names of all files attached to a PDF document using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Files can be attached at the document level or embedded in file attachment annotations.

Document-level attachments are accessible through the [PdfDocument.SharedAttachments property](https://api.docotic.com/pdfdocument-sharedattachments).

To list the names of files used in file attachment annotations, iterate through all pages of the document. File attachment annotations are accessible through the [PdfPage.Widgets property](https://api.docotic.com/pdfpage-widgets) and have the type [PdfFileAttachmentAnnotation](https://api.docotic.com/pdffileattachmentannotation).

Note that the same file attachment can be used in both a file attachment annotation and the document-level attachments collection. To identify unique attachments, compare their file specifications. You can compare file specifications using the `object.Equals` method or the `==` operator.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [File attachments](https://bitmiracle.com/pdf-library/edit/#attachments)