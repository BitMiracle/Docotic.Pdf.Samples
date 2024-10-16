# Extract attachments from PDF documents in C# and VB.NET
This sample shows how to save PDF attachment bytes using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Each attachment is represented by a [PdfFileSpecification object](https://api.docotic.com/pdffilespecification).
Use PdfFileSpecification.Contents property to retrieve a reference to a [PdfEmbeddedFile object](https://api.docotic.com/pdfembeddedfile).

If the retrieved reference is null then the file specification refers to an external file. I.e., only reference to a file is attached.

If the retrieved reference is non-null then the file is actually embedded in the document.
Use [PdfEmbeddedFile.Save methods](https://api.docotic.com/pdfembeddedfile-save) to extract contents of the attachment to a stream or a file.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [File attachments](https://bitmiracle.com/pdf-library/edit/#attachments)