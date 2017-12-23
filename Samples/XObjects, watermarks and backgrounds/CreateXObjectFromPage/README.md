# Create XObject from page
This sample shows how to create XObjects (often used for watermarks and backgrounds) based on existing PDF document pages.

Use PdfDocument.CreateXObject(PdfPage) method to create PdfXObject based on existing page. You can use a page from any PDF document (the one you will put the XObject in or other).

You can use created XObject as a watermark or background for a page using one of PdfCanvas.DrawXObject methods.

You can also use this technique to impose (combine) PDF pages onto larger (or same size) sheets to make books, booklets, or special arrangements. 