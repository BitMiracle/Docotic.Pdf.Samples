# Set up PDF permissions in C# and VB.NET

This sample shows how to set up user access permissions for a PDF document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Only encrypted documents can have user access permissions set up.

When encrypting with a password, there are two password types: "user" and "owner" passwords. Opening a PDF document with an "owner" password allows you to do everything with it. Opening a PDF document with a "user" password allows only operations specified by the user [access permissions](https://bitmiracle.com/pdf-library/api/pdfpermissions).

User access permissions may disallow printing of the document, filling in form fields, and other operations.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Encrypt PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/passwords/encrypt)
* [Encrypt PDF with a password in C# and VB.NET](/Samples/Security/SetPassword)
* [Protect PDF](https://bitmiracle.com/pdf-library/edit/#protect)