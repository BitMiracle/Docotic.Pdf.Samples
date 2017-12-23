# Extract images
This sample shows how to extract images from PdfDocument.

Use PdfImage.Save method to save image from PDF document to a stream or a file.

When saving to a file, you should specify an output file name without extension. PdfImage.Save adds extension based on the image data, saves image and returns full output path.

When saving to a stream, PdfImage.Save method returns format of the image data saved to a stream.