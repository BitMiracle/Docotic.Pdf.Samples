# OCR PDF and extract plain text in C# and VB.NET
This sample shows how to recognize and extract text from non-searchable PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) and [Tesseract OCR Engine](https://github.com/charlesw/tesseract).

Follow these steps to do OCR when a PDF page does not contain searchable text:
1. Save the page as high-resolution image using Docotic.Pdf. Higher resolution leads to better recognition quality.
2. Recognize the image using Tesseract OCR engine. 
3. Use recognized text.

If your documents contain text in language(s) other than English, provide [Language Data Files for Tesseract 4.00](https://github.com/tesseract-ocr/tessdata/tree/4.0.0) for the language(s) of your document.

Also ensure that you have [Visual Studio 2015-2019 x86 & x64 runtimes](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads) installed.

## See also
* [Get free time-limited license key for Docotic.Pdf](https://bitmiracle.com/pdf-library/download)
* [OCR PDF in C# and VB.NET](https://bitmiracle.com/blog/ocr-pdf-in-net)
* [Extract text from PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/pdf-text/extract)