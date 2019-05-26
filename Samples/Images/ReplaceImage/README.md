# Replace image
This sample shows how to replace existing images in PdfDocument.

Use PdfImage.ReplaceWith method to replace all occurrences of the image within PDF document.

This method throws UnsupportedImageException when used on an inline image. You can check PdfImage.IsInline property before invoking this method. Or use PdfCanvas.MoveInlineImagesToResources method before replacing.

Replacing an image can cause unexpected and/or unpleasant visual results. For example, image masking effects can be broken if you replace a mask image or an
image that has a mask.