# Save PDF page as image with specified resolution
This sample shows how to save a PDF page as image with specified resolution.

Use PdfDrawOptions.HorizontalResolution and PdfDrawOptions.VerticalResolution properties to set desired resolution (in pixels per inch) for output image. Then use configured PdfDrawOptions as parameter for PdfPage.Save() method.

Saving of PDF as image is not supported in version for .NET Standard.