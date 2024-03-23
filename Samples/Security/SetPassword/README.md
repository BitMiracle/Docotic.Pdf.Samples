# Encrypt PDF with a password in C# and VB.NET
This sample shows how to protect your PDF document with a password in C# and VB.NET.

When encrypting with a password, there are two password types: "user" and "owner" passwords. Opening a PDF document with an "owner" password allows you to do everything with it. Opening a PDF document with a "user" password allows only operations specified by the user access permissions. 

To protect the document, first, create an instance of [PdfStandardEncryptionHandler class](https://bitmiracle.com/pdf-library/api/pdfstandardencryptionhandler) with user and owner passwords. Then assign the handler to the [PdfSaveOptions.EncryptionHandler](https://bitmiracle.com/pdf-library/api/pdfsaveoptions-encryptionhandler) property.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Encrypt PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/passwords/encrypt) article
* [Permissions](/Samples/Security/Permissions) sample