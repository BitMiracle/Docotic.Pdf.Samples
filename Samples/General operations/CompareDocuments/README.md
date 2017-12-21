# Check that two PDF documents are equal
This sample shows how to check if two PDF documents are equal (i.e. have the same contents).

A PDF file consists of PDF objects. Some PDF objects contain unique data like content hash, creation time, etc. So you can not compare PDF files as byte streams because PDF documents with the same contents may have been saved with different compression, formatting and different unique data.

The PdfDocument.DocumentsAreEqual method compares two PDF documents as collections of PDF objects. It reports that documents are equal if all corresponding PDF objects are equal except some known fields (like CreationDate, or file IDs) that are really file properties and not contents properties.