# Remove XObjects
This sample shows how to remove XObjects (often used for watermarks and backgrounds) from PDF document pages. 

Use PdfPage.XObjects.Remove or PdfPage.XObjects.RemoveAt method to remove specified XObject from a page or PdfPage.XObjects.Clear method to remove all such objects.

Please note that XObjects are not always watermarks or backgrounds. Sometimes such objects are used for logos and other repeatable items.