# Open document encrypted with a certificate 
This sample shows how to open PDF documents protected with certificates.

If you have a matching certificate installed in the X.509 certificate store used by the current user, there is no need to do anything special. The library will find and use the certificate automatically. 

In other cases, you can create an instance of PdfPublicKeyDecryptionHandler class and use it to open the document. You can create PdfPublicKeyDecryptionHandler instances using a matching X509Certificate2 certificate, PFX / PKCS #12 key store, or X509Store store. In the latter case, the library will try to find a matching certificate in the store.

