# Test PDF processing in .NET and Native AOT builds

This sample shows how to automatically test PDF processing using [NUnit](https://nunit.org/) and [Docotic.Pdf](https://bitmiracle.com/pdf-library/) libraries.

Before running tests, please apply your permanent or temporary license key in the `Docotic.Tests\Helpers\DocoticLicense.cs` file.

The `Docotic.Tests` project contains tests for:
1. Opening and saving PDF documents. Resulting documents are compared using the [PdfDocument.DocumentsAreEqual](https://api.docotic.com/pdfdocument-documentsareequal)
2. Extraction of text from PDF documents.

It also runs the same set of tests for Native AOT version.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [C# Unit Testing. Why bother?](https://bitmiracle.com/pdf-library/howto/unit-testing) article