# Create LTV-enabled PDF signature in C# and VB.NET

This sample shows how to create a signature with LTV information using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Docotic.Pdf allows you to produce LTV-enabled signatures compliant with ETSI specifications.
First, you need to create PAdES B-T signature. Such signatures have ETSI.CAdES.detached format
and include a timestamp.

Then, use PdfDocument.AddLtvInfo methods to add LTV information to the signature. You need to
write the resulting PDF document incrementally to keep the signature valid.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [How to enable LTV in PDF documents](https://bitmiracle.com/pdf-library/signatures/ltv)
* [Certify PDF with LTV](/Samples/Digital%20signatures/CertifyPdfWithLtv)
* [Sign PDF document in C# and VB.NET](https://bitmiracle.com/pdf-library/signatures/sign)
* [Verify PDF signature in C# and VB.NET](https://bitmiracle.com/pdf-library/signatures/verify)
