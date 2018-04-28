# Save PDF page as image
This sample shows how to save a PDF page as image (i.e. how to convert a PDF page to raster format image).

Use PdfPage.Save method with specific PdfDrawOptions. To create PdfDrawOptions use PdfDrawOptions.Create() method. This method will create default options: PNG images with transparent background. Please note that you can fine-tune options after creation.