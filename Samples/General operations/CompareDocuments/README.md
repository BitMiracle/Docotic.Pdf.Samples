# Check if two PDF documents are equal in C# and VB.NET

This sample shows how to check if two PDF documents are equal / have the same contents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

A PDF file consists of PDF objects. Some PDF objects contain unique data like content hash, creation time, etc. PDF generators can save the same set of objects differently: with different compression, formatting, and different unique data. Because of this, you can not compare PDF files as byte streams.

The [PdfDocument.DocumentsAreEqual](https://api.docotic.com/pdfdocument-documentsareequal) methods compare two PDF documents as collections of PDF objects. They report if all corresponding PDF objects are equal. The comparison excludes some known fields (like CreationDate, or file IDs) that are really file properties and not content properties.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Check if PDF document is password protected using C# and VB.NET](/Samples/General%20operations/CheckIfPasswordProtected) sample