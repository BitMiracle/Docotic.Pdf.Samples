# Check if PDF document is password protected using C# and VB.NET

This sample shows how to use [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) to check if a PDF document is encrypted.

## Description

To get encryption information, use the [PdfDocument.GetEncryptionInfo](https://api.docotic.com/pdfdocument-getencryptioninfo) method. The method returns `null` (`Nothing` in VB.NET) for not encrypted documents. 

For documents encrypted with a password, the method returns an instance of the [PdfStandardEncryptionInfo](https://api.docotic.com/pdfstandardencryptioninfo) class. If the document is encrypted using a certificate, the `GetEncryptionInfo` method returns an instance of the [PdfPublicKeyEncryptionInfo](https://api.docotic.com/pdfpublickeyencryptioninfo) class.

The `GetEncryptionInfo` method is static, so you can perform a check before opening a PDF document. This is useful if you don't know beforehand if a password or a certificate is required to open the PDF document.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Decrypt PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/passwords/decrypt)
* [Password protection](https://bitmiracle.com/pdf-library/edit/#passwords)