# Extract painted images from PDF using C# and VB.NET

This sample shows how to extract images painted on a PDF page. [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) provides two different methods for extracting images from PDF files.

## Description 

Start by calling the [PdfPage.GetPaintedImages](https://api.docotic.com/pdfpage-getpaintedimages) method to get the collection of images drawn on a page. Then save the images from the collection “as is” and/or “as painted”.

When saving “as is“, the library does not change embedded image bytes at all. The extracted image may look different from what you see on the page. To save an image “as is”, access the [PdfImage](https://api.docotic.com/pdfimage) object using the [PdfPaintedImage.Image](https://api.docotic.com/pdfpaintedimage-image) property, then save the image using one of the [PdfImage.Save](https://api.docotic.com/pdfimage-save) methods.

When saving “as painted”, the library produces an image that looks the same as on the page. For this, the library applies all transformations specified for the image, like rotation, clipping, scaling, and flipping. 

To save an image “as painted”, use [PdfPaintedImage.SaveAsPainted](https://api.docotic.com/pdfpaintedimage-saveaspainted) methods. Some of these methods allow to specify the desired image format (PNG, JPEG or TIFF) and the resolution for the saved image. By default, the method produces PNG images with 72x72 ppi resolution.

The [PdfPaintedImage](https://api.docotic.com/pdfpaintedimage) class also provides the [TransformationMatrix](https://api.docotic.com/pdfpaintedimage-transformationmatrix) and the [IsTransformed](https://api.docotic.com/pdfpaintedimage-istransformed) properties. You can use these properties to check if the image on a page is painted scaled, flipped, or rotated.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Extract images from PDF in C# and VB.NET](/Samples/Images/ExtractImages) sample