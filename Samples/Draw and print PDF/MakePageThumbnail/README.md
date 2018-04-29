# Make page thumbnail
This sample shows how to make thumbnail of a PDF page.

Use PdfDrawOptions.CreateFitSize method to setup drawing options. This will cause page to be drawn as an image of the specified size. Then pass created PdfDrawOptions to PdfPage.Save method to save thumbnail of the page to a stream or a file.

There are also PdfDrawOptions.CreateFitWidth and PdfDrawOptions.CreateFitHeight methods. These methods are useful when you need thumbnail images of specified width or height and have no requirements for the second dimension of thumbnail images.