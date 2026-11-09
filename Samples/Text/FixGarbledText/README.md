# Fix garbled text when extracting text from PDF documents in C# and VB.NET

This sample shows how to extract text from PDF documents when standard text extraction methods produce garbled or unexpected results.

Some PDF documents look perfectly fine, and their text can be selected, copied, pasted, and searched. However, the extracted text does not correspond to what is visually displayed in the document. This is not a limitation of a particular text extraction tool: even Adobe tools cannot extract legible text from such documents.

The problem usually occurs when a PDF document contains missing or incorrect mappings between glyphs and Unicode characters.

The [PdfTextExtractionOptions.UnmappedCharacterHandler](https://api.docotic.com/pdftextextractionoptions-unmappedcharactercodehandler) property allows you to handle characters that cannot be mapped to Unicode. This sample demonstrates how to use OCR to recognize such characters and replace them with their correct Unicode values.

The sample uses the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) and the [Tesseract OCR Engine](https://github.com/charlesw/tesseract). You also need to install the [Visual C++ Redistributable for Visual Studio 2015–2022](https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist).

## See also
* [Get free time-limited license key for Docotic.Pdf](https://bitmiracle.com/pdf-library/download)
* [OCR PDF in C# and VB.NET](https://bitmiracle.com/blog/ocr-pdf-in-net)
* [Extract text from PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/pdf-text/extract)