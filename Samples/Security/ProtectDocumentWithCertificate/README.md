# Protect PDF document with a certificate in C# and VB.NET
This sample shows how to protect your PDF document with one or more certificates using the AES 256-bit encryption algorithm in C# and VB.NET.

Create an instance of [PdfPublicKeyEncryptionHandler class](https://bitmiracle.com/pdf-library/help/pdfpublickeyencryptionhandler.html) using an X509Certificate2 certificate or a key store. If you would like to limit permissions for the document, then use a constructor overload that accepts an instance of PdfPermissions class. 

Then specify PdfEncryptionAlgorithm.Aes256Bit for the Algorithm property of the handler. Finally, assign the handler to the [PdfSaveOptions.EncryptionHandler](https://bitmiracle.com/pdf-library/help/pdfsaveoptions.encryptionhandler.html) property to protect the output PDF file with advanced protection offered by AES 256-bit encryption.

To add more recipients, please use [AddRecipient](https://bitmiracle.com/pdf-library/help/pdfpublickeyencryptionhandler.addrecipient.html) and/or [AddOwner](https://bitmiracle.com/pdf-library/help/pdfpublickeyencryptionhandler.addowner.html) methods of PdfPublicKeyEncryptionHandler.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)
* [Encrypt PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/encrypt-pdf.aspx) article
* [Protect PDF document with AES](/Samples/Security/ProtectDocumentWithAes) sample