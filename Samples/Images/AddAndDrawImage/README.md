# Add and draw images
This samples shows how to use images in your PDF documents.

There are several ways to add an image to your PDF document. You can create an image from a file in GIF/Tiff/Png/Bmp/Jpeg formats, or a buffer of image bytes, or an System.Drawing.Image object. 

In order to add a new image to a PDF document, use PdfDocument.AddImage(...) method. After that use PdfCanvas.DrawImage(..) method to draw the added image on page or watermark. You can use overloads of PdfCanvas.DrawImage method to specify rotation angle or output size of the image to be drawn.

Each image can be drawn any number of times on any number of pages and with different output sizes or rotation angles.