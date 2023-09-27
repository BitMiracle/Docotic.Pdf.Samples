# List attachments and file annotations names in PDF document
This sample shows how to list names of all files attached to a PDF document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Attached files can be added to collection of document-level attachments or used in file attachment annotations.

Document level attachments are accessible through [PdfDocument.SharedAttachments property](https://bitmiracle.com/pdf-library/api/pdfdocument-sharedattachments).

In order to list names of all files used in file attachment annotations, you should loop through all document pages. File attachment annotations are accessible through [PdfPage.Widgets property](https://bitmiracle.com/pdf-library/api/pdfpage-widgets) and have type equal to [PdfFileAttachmentAnnotation](https://bitmiracle.com/pdf-library/api/pdffileattachmentannotation).

Note that the same attachment can be used in a file attachment annotation and added to collection of document-level annotations. You can find unique attachments by comparing their file specifications. File specifications can be compared using object.Equals method or simple "==" operator.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)