# Flatten form fields
This sample shows how to flatten form fields in a PDF document.

To flatten form fields in a PDF file please use PdfDocument.FlattenControls() method. This method draws each control on its parent page and then removes the control from the document.

Flattening locks form fields from editing and can significantly reduce output file size.