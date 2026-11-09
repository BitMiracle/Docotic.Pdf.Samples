# OCR PDF documents and make them searchable in C# and VB.NET

This sample shows how to convert image-based PDF documents into searchable PDFs using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) and the [Tesseract OCR Engine](https://github.com/charlesw/tesseract).

Follow these steps to make a PDF page searchable when it does not contain searchable text:

1. Use Docotic.Pdf to save the page as a high-resolution image. Higher image resolution generally improves OCR accuracy.
2. Pass the image to the Tesseract OCR Engine to recognize the text.
3. Use Docotic.Pdf to add the recognized text to the PDF page, making the document searchable while preserving its original appearance.

If your documents contain text in languages other than English, download the corresponding [language data files for Tesseract 4.00](https://github.com/tesseract-ocr/tessdata/tree/4.0.0) and make them available to the OCR engine.

You also need to install the [Visual C++ Redistributable for Visual Studio 2015–2022](https://learn.microsoft.com/en-us/cpp/windows/latest-supported-vc-redist).

## See also
* [Get free time-limited license key for Docotic.Pdf](https://bitmiracle.com/pdf-library/download)
* [OCR PDF in C# and VB.NET](https://bitmiracle.com/blog/ocr-pdf-in-net)
* [Extract text from PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/pdf-text/extract)