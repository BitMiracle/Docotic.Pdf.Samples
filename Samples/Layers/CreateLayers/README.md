# Add layers to PDF documents in C# and VB.NET

This sample shows how to create PDF layers (also known as optional content groups) using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

The `PdfDocument` class provides [CreateLayer](https://api.docotic.com/pdfdocument-createlayer) methods. You can use these methods to create layers in a PDF document. When you create a layer, the library automatically adds it to the collection of document layers.

You can use layers for canvas contents, for widgets, and for XObjects. This sample shows how to use [PdfCanvas.BeginMarkedContent methods](https://api.docotic.com/pdfcanvas-beginmarkedcontent) methods with layers.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add annotations and controls to layers](/Samples/Layers/AddAnnotationsAndControlsToLayers) sample
* [Add XObjects to layers](/Samples/Layers/AddXObjectsToLayers) sample