# Fix garbled text when extracting from PDF documents in C# and VB.NET
This sample shows how to OCR and extract garbled text from PDF document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) and [Tesseract OCR Engine](https://github.com/charlesw/tesseract).

There are searchable PDF documents with incorrect text. This happens when the document does not contain mappings of glyphs to Unicode. Or contains incorrect mappings.

Use [PdfTextExtractionOptions.UnmappedCharacterHandler](https://bitmiracle.com/pdf-library/help/pdftextextractionoptions.unmappedcharacterhandler.html)
property to skip or replace unmapped character codes. This sample shows how to perform OCR for unmapped character codes
and then replace them with correct Unicode values.

Also ensure that you have [Visual Studio 2015-2019 x86 & x64 runtimes](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads) installed.

## See also
* [OCR PDF in C# and VB.NET](https://bitmiracle.com/blog/ocr-pdf-in-net) article
* [Extract text from PDF in C# and VB.NET](https://bitmiracle.com/blog/extract-text-from-pdf-in-net) article
* [Get free time-limited license key for Docotic.Pdf](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)