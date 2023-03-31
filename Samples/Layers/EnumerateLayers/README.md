# Enumerate layers
This samples shows how to access and enumerate collection of PDF document layers (also known as optional content groups).

Use PdfDocument.Layers property to access the collection of document layers. This collection is enumerable. Each item
in the collection is a PdfLayer object. The PdfLayer object can be used to access layer properties like name, visibility state, and contents.
