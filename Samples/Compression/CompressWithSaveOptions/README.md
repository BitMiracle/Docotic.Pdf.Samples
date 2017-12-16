# Compress PDF document using save options
This sample shows how to optimize size of an existing PDF document using PdfDocument.SaveOptions property.

PDF file is internally a dump of PDF objects. Save options can be used to specify which optimizations should be applied to PDF objects while saving a PDF file. All these optimizations don't affect contents (text, images, bookmarks and anything else) of the PDF file. These optimizations only affect how PDF objects are written and compressed in an output PDF file.

Turning on all save options shown in this sample may help to achieve great compression ratio of the output PDF file but may also slow down saving of the document. This is especially true for large documents.