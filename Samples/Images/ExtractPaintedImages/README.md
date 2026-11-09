# Extract painted images from PDF using C# and VB.NET

This sample shows how to extract images painted on a PDF page using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

The library provides two ways to extract images from PDF documents: save embedded image data as is or save images as they appear on the page.

Start by calling the [PdfPage.GetPaintedImages](https://api.docotic.com/pdfpage-getpaintedimages) method to get a collection of images painted on the page. You can then save each image using either approach or both.

When saving an image **as is**, the library preserves the original embedded image data without applying any transformations. As a result, the extracted image may look different from how it appears on the PDF page. To save an image this way, access the [PdfImage](https://api.docotic.com/pdfimage) object through the [PdfPaintedImage.Image](https://api.docotic.com/pdfpaintedimage-image) property and call one of the [PdfImage.Save](https://api.docotic.com/pdfimage-save) methods.

When saving an image **as painted**, the library applies the transformations used to display the image on the page. These transformations can include rotation, clipping, scaling, and flipping. The resulting image looks as it appears on the PDF page.

To save an image as painted, call one of the [PdfPaintedImage.SaveAsPainted](https://api.docotic.com/pdfpaintedimage-saveaspainted) methods. Some overloads let you specify the output image format (PNG, JPEG, or TIFF) and resolution. By default, the method saves images in PNG format at 72 PPI.

The [PdfPaintedImage](https://api.docotic.com/pdfpaintedimage) class also provides the [TransformationMatrix](https://api.docotic.com/pdfpaintedimage-transformationmatrix) and [IsTransformed](https://api.docotic.com/pdfpaintedimage-istransformed) properties. Use these properties to determine whether an image is scaled, flipped, or rotated when painted on the page.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Extract images from PDF in C# and VB.NET](/Samples/Images/ExtractImages) sample