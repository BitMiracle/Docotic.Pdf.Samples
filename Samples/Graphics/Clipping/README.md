# Clipping
This sample shows how to use current graphics path as a clip region of a canvas.

After a graphics path is constructed, you can set it as a clip region of a canvas using PdfCanvas.SetClip method. After a clip region is set, you can fill or stroke it. You can also just continue drawing on a canvas.

When a clip region is set on a canvas, any part of a drawn object (including text and images) that lay outside the clip region will not be visible.