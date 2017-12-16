# List attachments and file annotations names
This sample shows how to list names of all files attached to a PdfDocument.

Attached files can be added to collection of document-level attachments or used in file attachment annotations.

Document level attachments are accessible through PdfDocument.SharedAttachments property.

In order to list names of all files used in file attachment annotations you should loop through all document pages. File attachment annotations are accessible through PdfPage.Widgets property and have type equal to PdfFileAttachmentAnnotation.

Note that the same attachment can be used in a file attachment annotation and added to collection of document-level annotations. You can find unique attachments by comparing their file specifications. File specifications can be compared using object.Equals method or simple "==" operator.