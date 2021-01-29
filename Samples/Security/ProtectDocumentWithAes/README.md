# Protect PDF document with AES in C# and VB.NET
This sample shows how to protect your PDF document with a password using the AES 256-bit encryption algorithm in C# and VB.NET.

Create an instance of [PdfStandardEncryptionHandler class](https://bitmiracle.com/pdf-library/help/pdfstandardencryptionhandler.html) with user and owner passwords. Then specify PdfEncryptionAlgorithm.Aes256Bit for the Algorithm property of the handler. Finally, assign the handler to the [PdfSaveOptions.EncryptionHandler](https://bitmiracle.com/pdf-library/help/pdfsaveoptions.encryptionhandler.html) property to protect the output PDF file with advanced protection offered by AES 256-bit encryption.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)
* [Encrypt and decrypt PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/passwords-and-security.aspx) article
* [Protect PDF document with a certificate](/Samples/Security/ProtectDocumentWithCertificate) sample