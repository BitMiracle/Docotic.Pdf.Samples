# OCR PDF and convert to searchable document in C# and VB.NET
This sample shows how to create searchable PDF document from a non-searchable (image-based) document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) and [Tesseract OCR Engine](https://github.com/charlesw/tesseract).

Follow these steps to do OCR when PDF page does not contain searchable text:
1. Save the page as high-resolution image using Docotic.Pdf. Higher resolution leads to better recognition quality.
2. Recognize the image using Tesseract OCR engine. 
3. Insert recognized text chunks back to PDF using Docotic.Pdf.

If your documents contain text in language(s) other than English, then make sure to provide [Language Data Files for Tesseract 4.00](https://github.com/tesseract-ocr/tessdata/tree/4.0.0) for the language(s) of your document. document.

Also ensure that you have [Visual Studio 2015-2019 x86 & x64 runtimes](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads) installed.

## See also
* [OCR PDF in C# and VB.NET](https://bitmiracle.com/blog/ocr-pdf-in-net) article
* [Extract text from PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/pdf-text/extract) article
* [Get free time-limited license key for Docotic.Pdf](https://bitmiracle.com/pdf-library/download)