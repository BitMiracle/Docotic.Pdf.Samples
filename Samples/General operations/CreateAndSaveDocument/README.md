# Create and save PDF document in C# and VB.NET

This sample shows how to create and save PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

The most important class in the library is [PdfDocument](https://api.docotic.com/pdfdocument). Each newly created PDF document already contains a page, you can access it using [PdfDocument.Pages](https://api.docotic.com/pdfdocument-pages) collection or [PdfDocument.GetPage](https://api.docotic.com/pdfdocument-getpage) method. You can save the completed document to a file or a stream using one of the [PdfDocument.Save](https://api.docotic.com/pdfdocument-save) methods.

The code also shows how to open the document immediately after saving. For this, the sample code uses `System.Diagnostics.Process` class and its `Start` method. As the result of the call, the operating system opens the PDF document in a default PDF viewer, if any.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Create PDF document in a Blazor WebAssembly application](/Samples/General%20operations/CreateDocumentFromBlazorWasm) sample