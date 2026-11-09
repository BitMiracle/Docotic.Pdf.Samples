# Check if a PDF document is password-protected in C# and VB.NET

This sample shows how to use the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) to check whether a PDF document is encrypted.

Use the [PdfDocument.GetEncryptionInfo](https://api.docotic.com/pdfdocument-getencryptioninfo) method to retrieve encryption information. The method returns `null` (`Nothing` in VB.NET) if the document is not encrypted.

For password-encrypted documents, the method returns an instance of the [PdfStandardEncryptionInfo](https://api.docotic.com/pdfstandardencryptioninfo) class. For documents encrypted with a certificate, it returns an instance of the [PdfPublicKeyEncryptionInfo](https://api.docotic.com/pdfpublickeyencryptioninfo) class.

The `GetEncryptionInfo` method is static, so you can check a document's encryption before opening it. This is useful when you don't know in advance whether opening the document requires a password or a certificate.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Decrypt PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/passwords/decrypt)
* [Password protection](https://bitmiracle.com/pdf-library/edit/#passwords)