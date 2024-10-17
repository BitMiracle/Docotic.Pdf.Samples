# Sign PDF document using AWS KMS
This sample shows how to sign a PDF document with a key from AWS Key Management Service in C# and VB.NET.

[Docotic.Pdf library](https://bitmiracle.com/pdf-library/) allows you to use non-exportable keys
from AWS to sign PDF documents. You need to implement `IPdfSigner` interface and use it in
[PdfSigningOptions](https://api.docotic.com/pdfsigningoptions).

This sample code automatically generates self-signed certificate for the provided AWS key.
In real applications, you might want to use a CA signed certificate.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Sign PDF document in C# and VB.NET](https://bitmiracle.com/pdf-library/signatures/sign)
* [Sign PDF document using Azure Key Vault](/Samples/Digital%20signatures/SignWithAzureKeyVault) sample
* [Sign PDF document using PKCS#11 driver](/Samples/Digital%20signatures/SignWithPkcs11) sample