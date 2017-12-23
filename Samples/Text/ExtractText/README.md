# Extract text
This sample shows how to extract text from a page or from entire PDF document.

Use PdfDocument.GetText() or PdfPage.GetText() methods to extract text in plain text format. You can also use PdfCanvas.GetTextData() method to extract text chunks with their coordinates.

Alternative methods are PdfDocument.GetTextWithFormatting() and PdfPage.GetTextWithFormatting(). These methods will extract text with formatting. Formatting means that all relative text positions will be kept after extraction and text will look more readable. Extracting text with formatting may be especially useful for PDF documents with tabular data.