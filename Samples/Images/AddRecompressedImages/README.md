# Add recompressed images
This sample shows how to change images compression while adding them to a PDF document.

To recompress an image or a frame of an image before adding it to a PDF document, open the image using PdfDocument.OpenImage method. Then setup compression options for all or selected frames. If needed, each frame will be recompressed before adding it to document.

You can compress images using Flate, Jpeg and CCITT compression schemes. You may even completely decompress an image if you wish or have to do so.

You can also recompress existing images in a PDF document. Please look at "Compression / Optimize images in PDF document" sample for more detail.