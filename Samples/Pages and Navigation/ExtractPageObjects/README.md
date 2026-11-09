# Extract PDF text, paths, and images in C# and VB.NET

This sample shows how to extract text, paths, and images from PDF pages and draw them on a `System.Drawing.Graphics` surface.

Use one of the [PdfPage.GetObjects](https://api.docotic.com/pdfpage-getobjects) methods to retrieve the text, paths, and images drawn on a page. These methods return the objects in drawing order. The sample iterates through the collection and draws each object after the preceding ones, preserving their original stacking order.

The sample uses the free [Docotic.Pdf.Gdi add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Gdi) for the Docotic.Pdf library.

**Note:** The add-on is not recommended for use in certain environments. See the add-on description for more information.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Copy text, paths and images between PDF pages in C# and VB.NET](/Samples/Pages%20and%20Navigation/CopyPageObjects)
