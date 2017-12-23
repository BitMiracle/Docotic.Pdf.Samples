# Copy text, paths and images
This sample shows how to copy page objects (text, paths and images) to a new document.

Use PdfDocument.CopyPage method to copy full page content and associated resources (fonts, images) to a new document. Then use PdfPage.GetObjects() method to get all text, paths and images drawn on the page. Copy required objects to a new page. At the end, remove original page from the result document.

Copying of page objects is useful when you want to remove or replace some content (e.g. text) from the page.