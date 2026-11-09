# Enumerate PDF layers in C# and VB.NET

This sample shows how to access and enumerate PDF document layers (also known as optional content groups) using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Use the `PdfDocument.OptionalContent` property to access optional content groups and their configuration. The `OptionalContent.Layers` collection provides access to the document's layers. Each item in the collection is a [PdfLayer](https://api.docotic.com/pdflayer) object. You can use these objects to retrieve layer information, such as names, visibility states, and intents.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Add layers to PDF documents in C# and VB.NET](/Samples/Layers/CreateLayers) sample