# Add attachments and file annotations to PDF documents in C# and VB.NET
This sample shows how to attach files to a PDF document or add file attachment annotations using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use one of the [PdfDocument.CreateFileAttachment methods](https://api.docotic.com/pdfdocument-createfileattachment) to create a file attachment.

You can then add the attachment to the document-level attachments collection using [PdfDocument.SharedAttachments](https://api.docotic.com/pdfdocument-sharedattachments). Alternatively, you can add it as a file attachment annotation using the [PdfPage.AddFileAnnotation](https://api.docotic.com/pdfpage-addfileannotation) method.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [File attachments](https://bitmiracle.com/pdf-library/edit/#attachments)