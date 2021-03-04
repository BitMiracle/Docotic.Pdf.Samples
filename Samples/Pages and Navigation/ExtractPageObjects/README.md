# Extract text, paths and images
This sample shows how to extract page objects (text, paths and images) from PdfDocument. The sample draws extracted objects on the drawing surface of a System.Drawing.Graphics.

Use PdfPage.GetObjects() method to get all text, paths and images drawn on the page.

The result of this method is an ordered collection. Each subsequent object in the collection is drawn after the previous one.

This sample code uses free [Docotic.Pdf.Gdi add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Gdi) for Docotic.Pdf library. In certain environments, it is not recommended to use the add-on. Please read the description for the addon-on for more information.