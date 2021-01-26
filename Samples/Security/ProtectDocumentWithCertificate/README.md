# Protect PDF document with a certificate
This sample shows how to protect your PDF document with one or more certificates using AES 256-bit encryption algorithm.

Create an instance of PdfPublicKeyEncryptionHandler class using an X509Certificate2 certificate, or a key store. If you would like to limit permissions for the document, then use a constructor overload that accepts an instance of PdfPermissions class. 

Then specify PdfEncryptionAlgorithm.Aes256Bit for the Algorithm property of the handler. Finally, assign the handler to the PdfSaveOptions.EncryptionHandler property to protect the output PDF file with advanced protection offered by AES 256-bit encryption.

To add more recipients, please use AddRecipient and/or AddOwner methods of PdfPublicKeyEncryptionHandler.