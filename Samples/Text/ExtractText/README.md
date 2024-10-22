# Convert PDF to text in C# and VB.NET
This sample shows how to extract text from a PDF document or from a PDF page in C# and VB.NET.

Use [PdfDocument.GetText](https://api.docotic.com/pdfdocument-gettext) or [PdfPage.GetText](https://api.docotic.com/pdfpage-gettext) methods to extract text in plain text format. You can also use [PdfCanvas.GetTextData method](https://api.docotic.com/pdfcanvas-gettextdata) to extract text chunks with their coordinates.

The alternative methods are [PdfDocument.GetTextWithFormatting](https://api.docotic.com/pdfdocument-gettextwithformatting) and [PdfPage.GetTextWithFormatting](https://api.docotic.com/pdfpage-gettextwithformatting). These methods extract text with formatting. Formatting means that all relative text positions will be kept after extraction and the text will look more readable. Extracting text with formatting may be especially useful for PDF documents with tabular data.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Extract text from PDF in C# and VB.NET](https://bitmiracle.com/pdf-library/pdf-text/extract)
* [Find and highlight text in a PDF document](/Samples/Text/FindAndHighlightText) sample