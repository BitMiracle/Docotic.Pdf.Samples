# Extract text, paths and images
This sample shows how to extract page objects (text, paths and images) from PdfDocument. The sample draws extracted objects on the drawing surface of a System.Drawing.Graphics.

Use PdfPage.GetObjects() method to get all text, paths and images drawn on the page.

The result of this method is an ordered collection. Each subsequent object in the collection is drawn after the previous one.