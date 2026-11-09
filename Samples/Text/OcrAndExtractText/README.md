# OCR PDF documents and extract plain text in C# and VB.NET

This sample shows how to recognize text in non-searchable PDF documents and extract the recognized text using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) and the [Tesseract OCR Engine](https://github.com/charlesw/tesseract).

Follow these steps to perform OCR on a PDF page that does not contain searchable text:

1. Use Docotic.Pdf to save the page as a high-resolution image. Higher image resolution generally improves OCR accuracy.
2. Pass the image to the Tesseract OCR Engine to recognize the text.
3. Retrieve and use the recognized text.

If your documents contain text in languages other than English, download the corresponding [language data files for Tesseract 4.00](https://github.com/tesseract-ocr/tessdata/tree/4.0.0) and make them available to the OCR engine.

You also need to install the [Visual C++ Redistributable for Visual Studio 2015–2022](https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist).

## See also
* [Get free time-limited license key for Docotic.Pdf](https://bitmiracle.com/pdf-library/download)
* [OCR PDF in C# and VB.NET](https://bitmiracle.com/blog/ocr-pdf-in-net)
* [Extract text from PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/pdf-text/extract)