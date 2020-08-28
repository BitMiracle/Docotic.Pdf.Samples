# Convert PDF to text in C# and VB.NET
This sample shows how to extract text from PDF document or from a PDF page in C# and VB.NET.

Use [PdfDocument.GetText()](https://bitmiracle.com/pdf-library/help/pdfdocument.gettext.html) or [PdfPage.GetText()](https://bitmiracle.com/pdf-library/help/pdfpage.gettext.html) methods to extract text in plain text format. You can also use [PdfCanvas.GetTextData() method](https://bitmiracle.com/pdf-library/help/pdfcanvas.gettextdata.html) to extract text chunks with their coordinates.

Alternative methods are [PdfDocument.GetTextWithFormatting()](https://bitmiracle.com/pdf-library/help/pdfdocument.gettextwithformatting.html) and [PdfPage.GetTextWithFormatting()](https://bitmiracle.com/pdf-library/help/pdfpage.gettextwithformatting.html). These methods will extract text with formatting. Formatting means that all relative text positions will be kept after extraction and text will look more readable. Extracting text with formatting may be especially useful for PDF documents with tabular data.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)
* [Extract text from PDF in C# and VB.NET](https://bitmiracle.com/blog/extract-text-from-pdf-in-net) article
* [Find and highlight text in a PDF document](/Samples/Text/FindAndHighlightText) sample