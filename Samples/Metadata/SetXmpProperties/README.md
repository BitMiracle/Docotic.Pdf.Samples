# Set XMP metadata properties
This sample shows how to change XMP metadata properties (like Dublin Core properties) of a PDF document. These properties are displayed in the Additional Metadata view of Document Properties dialog of Adobe Acrobat Professional.

Docotic.Pdf can be used to read and set XMP metadata properties defined in any schema. For some schemas (so called "well-known schemas") there are convenience properties added to the API of the library. 

To access metadata properties of a PDF document, use PdfDocument.Metadata property.

PdfDocument.Metadata also contains convenience properties for custom (application-defined) properties, Adobe PDF file properties, XMP Core / XMP Basic properties, Dublin Core properties, Media Management properties and Rights Management properties. 