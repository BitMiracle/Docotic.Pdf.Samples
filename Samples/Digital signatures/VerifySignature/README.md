# Verify PDF signature in C# and VB.NET
This sample shows how to verify a PDF signature and to check revocation of its signing certificate.

Docotic.Pdf library provides means to check if the signed part of a PDF document was changed after signing. You can check if a signature contains embedded OCSP and/or CRL data. And for any signature, it is possible to check if its signing certificate is revoked for any given date.