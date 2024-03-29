# Protect PDF document with a certificate in C# and VB.NET
This sample shows how to protect your PDF document with one or more certificates using the AES 256-bit encryption algorithm in C# and VB.NET.

Create an instance of [PdfPublicKeyEncryptionHandler class](https://bitmiracle.com/pdf-library/api/pdfpublickeyencryptionhandler) using an X509Certificate2 certificate or a key store. If you would like to limit permissions for the document, then use a constructor overload that accepts an instance of PdfPermissions class.

Then specify PdfEncryptionAlgorithm.Aes256Bit for the Algorithm property of the handler. Finally, assign the handler to the [PdfSaveOptions.EncryptionHandler](https://bitmiracle.com/pdf-library/api/pdfsaveoptions-encryptionhandler) property to protect the output PDF file with advanced protection offered by AES 256-bit encryption.

To add more recipients, please use [AddRecipient](https://bitmiracle.com/pdf-library/api/pdfpublickeyencryptionhandler-addrecipient) and/or [AddOwner](https://bitmiracle.com/pdf-library/api/pdfpublickeyencryptionhandler-addowner) methods of PdfPublicKeyEncryptionHandler.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Encrypt PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/passwords/encrypt) article
* [Protect PDF document with AES](/Samples/Security/ProtectDocumentWithAes) sample