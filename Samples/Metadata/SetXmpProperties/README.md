# Change XMP metadata of a PDF in C# and VB.NET

This sample shows how to change properties in XMP metadata of a PDF document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/). 

## Description

The library can read and set XMP metadata properties defined in any schema. For some schemas, so called "well-known schemas", there are convenience properties added to the API of the library. 

To access metadata properties of a PDF document, use the [PdfDocument.Metadata](https://api.docotic.com/pdfdocument-metadata) property. The property value is an object of the [XmpMetadata](https://api.docotic.com/xmpmetadata) class. 

The `XmpMetadata` class contains convenience properties for these schemas:
* [Adobe PDF](https://api.docotic.com/xmpmetadata-pdf)
* [XMP Core / XMP Basic](https://api.docotic.com/xmpmetadata-basic)
* [Dublin Core](https://api.docotic.com/xmpmetadata-dublincore)
* [Media Management](https://api.docotic.com/xmpmetadata-mediamanagement)
* [Rights Management](https://api.docotic.com/xmpmetadata-rightsmanagement)
* schema with [custom, application-defined properties](https://api.docotic.com/xmpmetadata-basic)

You can access other schemas by using the [XmpMetadata.Schemas](https://api.docotic.com/xmpmetadata-schemas) property.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Metadata in PDF](https://bitmiracle.com/pdf-library/edit/#metadata)
* [Set custom XMP metadata properties in C# and VB.NET](/Samples/Metadata/SetCustomXmpProperties)