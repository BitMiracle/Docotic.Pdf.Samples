# Change XMP metadata in a PDF in C# and VB.NET

This sample shows how to read and modify XMP metadata properties of a PDF document using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

Docotic.Pdf supports reading and writing XMP metadata properties defined in any schema. For some commonly used schemas, known as *well-known schemas*, the library provides dedicated convenience properties.

To access a PDF document's metadata, use the [PdfDocument.Metadata](https://api.docotic.com/pdfdocument-metadata) property. It returns an [XmpMetadata](https://api.docotic.com/xmpmetadata) object.

The `XmpMetadata` class provides convenience properties for the following schemas:
* [Adobe PDF](https://api.docotic.com/xmpmetadata-pdf)
* [XMP Core / XMP Basic](https://api.docotic.com/xmpmetadata-basic)
* [Dublin Core](https://api.docotic.com/xmpmetadata-dublincore)
* [Media Management](https://api.docotic.com/xmpmetadata-mediamanagement)
* [Rights Management](https://api.docotic.com/xmpmetadata-rightsmanagement)
* [Custom, application-defined properties](https://api.docotic.com/xmpmetadata-basic)

To access properties defined in other schemas, use the [XmpMetadata.Schemas](https://api.docotic.com/xmpmetadata-schemas) property.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Metadata in PDF](https://bitmiracle.com/pdf-library/edit/#metadata)
* [Set custom XMP metadata properties in C# and VB.NET](/Samples/Metadata/SetCustomXmpProperties)