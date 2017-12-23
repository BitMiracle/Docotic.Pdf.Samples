# Image masks
This sample shows how to use mask images.

You can add a transparent image to your PDF document using PdfDocument.AddImage method with a mask image as a parameter. Mask image must have the same width and height as an image to be made transparent and their pixel data must be encoded using 1 bit per pixel.

All pixels in the resulting image that correspond to the black pixels of a mask image will be drawn unchanged. All pixels in the resulting image that correspond to the white pixels of a mask image will not be drawn. Black pixels in a mask image are encoded using '0' bits. White pixels in a mask image are encoded using '1' bits.