# Fix garbled text when extracting from PDF documents in C# and VB.NET
This sample shows how to extract text from PDF documents when regular methods produce garbled / unexpected text. 

There are searchable PDF documents that look just fine. But it’s not possible to copy or extract text from them properly. Even by Adobe tools. 

This happens when the document does not contain mappings of glyphs to Unicode characters. Or contains incorrect mappings.

There is [PdfTextExtractionOptions.UnmappedCharacterHandler](https://bitmiracle.com/pdf-library/help/pdftextextractionoptions.unmappedcharacterhandler.html) property. This sample shows how to perform OCR for unmapped characters and then replace them with correct Unicode values.

This sample uses [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) and [Tesseract OCR Engine](https://github.com/charlesw/tesseract). You would also need to have [Visual Studio 2015-2019 x86 & x64 runtimes](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads) installed.

## See also
* [OCR PDF in C# and VB.NET](https://bitmiracle.com/blog/ocr-pdf-in-net) article
* [Extract text from PDF in C# and VB.NET](https://bitmiracle.com/blog/extract-text-from-pdf-in-net) article
* [Get free time-limited license key for Docotic.Pdf](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)