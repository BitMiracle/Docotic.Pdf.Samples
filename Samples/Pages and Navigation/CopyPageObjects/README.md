# Copy text, paths and images between PDF pages in C# and VB.NET
This sample shows how to copy page objects (text, paths and images) to a new PDF page using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

Use [PdfDocument.CopyPages](https://api.docotic.com/pdfdocument-copypages) methods to copy complete page content and associated resources like fonts and images to a new document. Then use one of the [PdfPage.GetObjects](https://api.docotic.com/pdfpage-getobjects) methods to get all page text, paths, and images. Copy the needed objects to a new page. At the end, remove the original page from the document.

You can remove or replace some content (e.g., text) from a page by copying only some of the page objects.

This sample does not preserve all structure information because it copies page objects to another page. Look at the [Edit PDF page content](/Samples/Pages%20and%20Navigation/EditPageContent) sample that shows how to copy page objects to the same page and preserve structure information.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Remove paths](/Samples/Graphics/RemovePaths) sample
* [Remove painted images](/Samples/Images/RemovePaintedImages) sample
* [Extract PDF text, paths and images in C# and VB.NET](/Samples/Pages%20and%20Navigation/ExtractPageObjects) sample