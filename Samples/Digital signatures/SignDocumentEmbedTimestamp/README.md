# Sign PDF document and embed a timestamp in C# and VB.NET

This sample shows how to sign an existing PDF document and embed a timestamp into the signature. 

When signing a document, [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) can embed a timestamp into the signature.
That allows to create PAdeS B-T signatures according to ETSI specifications.

The library gets timestamp from the Timestamp Authority specified as a signing option.
It is also possible to specify username and password for the Timestamp Authority, if needed.

For compatibility reasons, it is recommended to use one of the SHA digest algorithms for timestamped signatures.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Sign PDF document in C# and VB.NET](https://bitmiracle.com/pdf-library/signatures/sign)
* [Verify PDF signature in C# and VB.NET](https://bitmiracle.com/pdf-library/signatures/verify)
* [Create LTV-enabled PDF signature](/Samples/Digital%20signatures/CreateLtvSignature)