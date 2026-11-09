# Add layers to PDF documents in C# and VB.NET

This sample shows how to create PDF layers (also known as optional content groups) using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

The `PdfDocument` class provides access to optional content groups and their configuration through the `OptionalContent` property. The `OptionalContent.Layers` collection provides access to the document's layers. Use the collection's `Add` methods to create new layers.

You can associate canvas content, widgets, and XObjects with layers. This sample demonstrates how to associate canvas content with a layer using the [PdfCanvas.BeginMarkedContent methods](https://api.docotic.com/pdfcanvas-beginmarkedcontent).

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add annotations and controls to layers](/Samples/Layers/AddAnnotationsAndControlsToLayers) sample
* [Add XObjects to layers](/Samples/Layers/AddXObjectsToLayers) sample