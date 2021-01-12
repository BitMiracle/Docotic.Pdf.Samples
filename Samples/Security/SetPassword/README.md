# Set password
This sample shows how to protect your PDF document with a password.

When encrypting with a password, there are two password types: "user" and "owner" passwords. Opening a PDF document with an "owner" password allows to do everything with the opened document. Opening a PDF document with a "user" password allows only operations specified by the user access permissions. 

To protect the document, first create an instance of PdfStandardEncryptionHandler class with user and owner passwords. Then assign the handler to the PdfSaveOptions.EncryptionHandler proprety. 