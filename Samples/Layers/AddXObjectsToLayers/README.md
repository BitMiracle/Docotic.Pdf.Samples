# Add XObjects to PDF layers in C# and VB.NET

This sample shows how to add XObjects to layers in a PDF document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

The [PdfXObject](https://api.docotic.com/pdfxobject) class has the [Layer](https://api.docotic.com/pdfxobject-layer) property. To associate an XObject with a layer assign the layer to the property.

To remove a XObject from a layer, you only need to set the `XObject.Layer` property to `null`.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add annotations and controls to PDF layers in C# and VB.NET](/Samples/Layers/AddAnnotationsAndControlsToLayers) sample