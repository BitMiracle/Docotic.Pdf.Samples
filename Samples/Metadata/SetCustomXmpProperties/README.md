# Set custom XMP metadata properties in C# and VB.NET

This sample shows how to set custom, application-defined metadata properties in a PDF document using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Docotic.Pdf supports reading and writing custom properties that contain strings, arrays of various types, and structures. To access a PDF document's custom metadata properties, first obtain the [XmpMetadata](https://api.docotic.com/xmpmetadata) object through the [PdfDocument.Metadata](https://api.docotic.com/pdfdocument-metadata) property. Then use the object's [Custom](https://api.docotic.com/xmpmetadata-custom) property to access the custom properties.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Metadata in PDF](https://bitmiracle.com/pdf-library/edit/#metadata)
* [Change XMP metadata of a PDF in C# and VB.NET](/Samples/Metadata/SetXmpProperties)