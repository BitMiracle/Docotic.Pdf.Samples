# Replace image
This sample shows how to replace images in PDF documents using C# or VB.NET and [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

Use one of the [PdfImage.ReplaceWith](https://api.docotic.com/pdfimage-replacewith) methods to replace all occurrences of the image within the PDF document.

These methods throw an [UnsupportedImageException](https://api.docotic.com/unsupportedimageexception) when used on an inline image. To avoid the exception, check the [PdfImage.IsInline](https://api.docotic.com/pdfimage-isinline) property before replacing the image. You can convert inline images using one of the [PdfCanvas.MoveInlineImagesToResources](https://api.docotic.com/pdfcanvas-moveinlineimagestoresources) methods before replacing any of them.

Replacing an image can cause unexpected and/or unpleasant visual results. For example, if you replace a mask image or an image that has a mask, it can break image masking effects.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Remove and replace images](https://bitmiracle.com/pdf-library/edit/#remove-images)