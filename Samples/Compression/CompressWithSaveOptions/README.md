# Optimize PDF objects in C# and VB.NET
This sample shows how to optimize the size of an existing PDF document using [save options](https://api.docotic.com/pdfsaveoptions).

A PDF file is internally a dump of PDF objects. You can use save options to specify which optimizations should be applied to PDF objects while saving a PDF file. All these optimizations don't affect the contents (text, images, bookmarks, and anything else) of the PDF file. These optimizations only affect how PDF objects are written and compressed in an output PDF file.

Turning on all save options shown in this sample may help achieve a great compression ratio of the output PDF file but may also slow down the saving of the document. It is especially true for large documents.

This sample shows only one approach to reduce the size of a PDF. Please check [Compress PDF documents in C# and VB.NET](/Samples/Compression/CompressAllTechniques) sample code to see more approaches.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Compress PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/optimize/compress)
* [Save options](https://bitmiracle.com/pdf-library/edit/#save-options)