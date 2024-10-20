# Extract images from PDF in C# and VB.NET

This sample shows how to extract images from a PDF document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use [PdfImage.Save](https://api.docotic.com/pdfimage-save) methods to save an image from a PDF document to a stream or a file.

When saving to a file, you must specify an output file name without extension. The `PdfImage.Save` method will add an extension based on the image data, save the image, and return the full output path.

When saving to a stream, the `PdfImage.Save` method returns the format of the image data saved to a stream.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Extract PDF images](https://bitmiracle.com/pdf-library/edit/#extract-images)
* [Extract PDF image coordinates in C# and VB.NET](/Samples/Images/ExtractImageCoordinates) sample