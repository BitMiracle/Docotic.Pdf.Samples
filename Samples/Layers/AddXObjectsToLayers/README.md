# Add XObjects to PDF layers in C# and VB.NET

This sample shows how to associate XObjects with layers in a PDF document using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

The [PdfXObject](https://api.docotic.com/pdfxobject) class exposes a [Layer](https://api.docotic.com/pdfxobject-layer) property. To associate an XObject with a layer, assign the layer to this property.

To remove an XObject from a layer, set its `Layer` property to `null`.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add annotations and controls to PDF layers in C# and VB.NET](/Samples/Layers/AddAnnotationsAndControlsToLayers) sample