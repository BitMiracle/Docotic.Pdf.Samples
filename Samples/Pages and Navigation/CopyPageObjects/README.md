# Copy text, paths, and images between PDF pages in C# and VB.NET

This sample shows how to copy text, paths, and images from one PDF page to another using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use one of the `CopyRange` methods of the `PdfDocument.Pages` collection to copy the entire page content, along with associated resources such as fonts and images, to a new document. Then, use one of the [PdfPage.GetObjects](https://api.docotic.com/pdfpage-getobjects) methods to retrieve the text, paths, and images from the copied page. Copy the objects you want to keep to another page in the new document. Finally, remove the intermediate page.

You can use this approach to remove or replace specific page content, such as text, by copying only the page objects you want to retain.

Note that this sample does not preserve all structural information because it copies page objects to a different page. See the [Edit PDF page content](/Samples/Pages%20and%20Navigation/EditPageContent) sample for an example of copying page objects within the same page while preserving structural information.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Remove paths](/Samples/Graphics/RemovePaths) sample
* [Remove painted images](/Samples/Images/RemovePaintedImages) sample
* [Extract PDF text, paths and images in C# and VB.NET](/Samples/Pages%20and%20Navigation/ExtractPageObjects) sample