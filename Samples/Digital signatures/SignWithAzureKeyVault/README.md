# Sign PDF document using Azure Key Vault
This sample shows how to sign a PDF document with a key from Azure Key Vault in C# and VB.NET.

[Docotic.Pdf library](https://bitmiracle.com/pdf-library/) allows you to use non-exportable keys
from Azure to sign PDF documents. You need to implement `IPdfSigner` interface and use it in
[PdfSigningOptions](https://api.docotic.com/pdfsigningoptions).

This sample code automatically generates self-signed certificate for the provided Azure key.
In real applications, you might want to use a CA signed certificate. You can also load a certificate
from Key Vault secrets or Key Vault certificates.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Sign PDF document in C# and VB.NET](https://bitmiracle.com/pdf-library/signatures/sign)
* [Sign PDF document using AWS KMS](/Samples/Digital%20signatures/SignWithAwsKms) sample
* [Sign PDF document using PKCS#11 driver](/Samples/Digital%20signatures/SignWithPkcs11) sample