# Extract PDF text, paths and images in C# and VB.NET

This sample shows how to extract page objects like text, paths, and images from a PDF document.

## Description 

The sample draws the extracted objects on a System.Drawing.Graphics surface.

Use [PdfPage.GetObjects](https://api.docotic.com/pdfpage-getobjects) methods to get all page text, paths, and images. The methods return an ordered collection. The code draws each subsequent object in the collection after the previous one.

This sample code uses free [Docotic.Pdf.Gdi add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Gdi) for Docotic.Pdf library. 

NOTE: In certain environments, it is not recommended to use the add-on. Please read the description for the addon for more information.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Copy text, paths and images between PDF pages in C# and VB.NET](/Samples/Pages%20and%20Navigation/CopyPageObjects)
