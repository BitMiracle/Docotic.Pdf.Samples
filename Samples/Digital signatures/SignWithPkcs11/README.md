# Sign PDF document using PKCS#11 driver in C# and VB.NET
This sample shows how to sign a PDF document with a key from a PKCS#11 device using
[Docotic.Pdf](https://bitmiracle.com/pdf-library/) and [Pkcs11Interop](https://pkcs11interop.net/) libraries.

Docotic.Pdf allows you to sign PDF documents using smart keys, USB tokens, and HSM devices.
You need to implement `IPdfSigner` interface and use it in [PdfSigningOptions](https://bitmiracle.com/pdf-library/api/pdfsigningoptions).

Pkcs11Interop library helps to work with unmanaged PKCS#11 libraries in .NET.

## See also
* [Get free time-limited license key for Docotic.Pdf](https://bitmiracle.com/pdf-library/download)
* [Sign PDF document in C# and VB.NET](https://bitmiracle.com/pdf-library/signatures/sign) article
* [Sign PDF document using AWS KMS](/Samples/Digital%20signatures/SignWithAwsKms) sample
* [Sign PDF document using Azure Key Vault](/Samples/Digital%20signatures/SignWithAzureKeyVault) sample