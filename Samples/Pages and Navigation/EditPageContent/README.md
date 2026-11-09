# Edit PDF page content in C# and VB.NET

This sample shows how to modify PDF page content using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use one of the [PdfPage.GetObjects](https://api.docotic.com/pdfpage-getobjects) methods to retrieve the text, paths, and images drawn on a page. Copy the objects you want to keep to a temporary page, modifying them as needed during the copying process. Then, replace the source page's content with the content of the temporary page.

In this sample, we change the orange color at the top of the page to gray and remove all text on the right side.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Remove paths](/Samples/Graphics/RemovePaths) sample
* [Remove painted images](/Samples/Images/RemovePaintedImages) sample
* Other [PDF editing features](https://bitmiracle.com/pdf-library/edit/) for your C# and VB.NET solutions