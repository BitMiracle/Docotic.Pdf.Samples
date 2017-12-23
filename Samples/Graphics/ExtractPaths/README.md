# Extract paths
This sample shows how to extract vector paths from a page and copy them to a new document.

Use PdfPath objects in PdfPage.GetObjects() resulting collection to extract vector paths. A path consists of one or more disconnected subpaths. Each subpath comprises a sequence of connected segments.