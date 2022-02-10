# Extract painted images
This sample shows how to extract images painted on page in a PDF document using two different methods.

You can use PdfPage.GetPaintedImages() method to get all images drawn on the page. You can save these images "as is" (as they are stored in a PDF document) and "as painted" (as they are painted on a page).

To save an image "as is", you will need to access PdfImage object using PdfPaintedImage.Image property, then save the accessed image using PdfImage.Save method.

To save an image "as painted", you will need to use PdfPaintedImage.SaveAsPainted method. This method saves the image as painted on a page (i.e. taking into account whether image is rotated, flipped, or scaled).  
Using SaveAsPainted method, you can specify desired image format (PNG, JPEG or TIFF) and resolution for the saved image. By default, the method produces PNG images with 72x72 ppi resolution.

The PdfPaintedImage class also provides TransformationMatrix and IsTransformed properties. You can use them to determine whether the image is painted on a page scaled, flipped, and / or rotated.