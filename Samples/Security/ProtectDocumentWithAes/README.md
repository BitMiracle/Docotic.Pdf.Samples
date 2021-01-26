# Protect PDF document with AES
This sample shows how to protect your PDF document with a password using AES 256-bit encryption algorithm.

Create an instance of PdfStandardEncryptionHandler class with user and owner passwords. Then specify PdfEncryptionAlgorithm.Aes256Bit for the Algorithm property of the handler. Finally, assign the handler to the PdfSaveOptions.EncryptionHandler property to protect the output PDF file with advanced protection offered by AES 256-bit encryption.