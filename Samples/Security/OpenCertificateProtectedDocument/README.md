# Open PDF document encrypted with a certificate in C# and VB.NET
This sample shows how to open PDF documents protected with certificates in C# and VB.NET.

If you have a matching certificate installed in the X.509 certificate store used by the current user, there is no need to do anything special. [The library](https://bitmiracle.com/pdf-library/) will find and use the certificate automatically. 

In other cases, you can create an instance of [PdfPublicKeyDecryptionHandler class](https://bitmiracle.com/pdf-library/help/pdfpublickeydecryptionhandler.html) and use it to open the document. You can create PdfPublicKeyDecryptionHandler instances using a matching X509Certificate2 certificate, PFX / PKCS #12 key store, or X509Store store. In the latter case, the library will try to find a matching certificate in the store.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)
* [Encrypt and decrypt PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/passwords-and-security.aspx) article
