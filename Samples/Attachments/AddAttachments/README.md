# Add attachments and file annotations to PDF
This samples shows how to attach a file or add a file annotation to your PDF document.

Use PdfDocument.CreateFileAttachment(..) method to create a new attachment.

You can add created attachment to collection of document-level attachments using PdfDocument.SharedAttachments.Add(..) method. Or you can add created attachment as a file attachment annotation using PdfPage.AddFileAnnotation(..) method.