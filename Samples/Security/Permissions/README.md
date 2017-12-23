# Permissions
This sample shows how to setup user acess permissions for a PDF document.

There are two passwords types: a "user" and an "owner" password. PdfDocument.OwnerPassword property is for an "owner" password and PdfDocument.UserPassword property is for a "user" password.

Opening a PDF document with an "owner" password allows a reader of your document to do everything with opened document. Opening a PDF document with a "user" password allows a reader of your document to only perform operation allowed by user access permissions. 

User access permissions may be used to disallow printing of document, filling of form fields or other operations.