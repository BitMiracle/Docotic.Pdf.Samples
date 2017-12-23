# Add watermarks and backgrounds
This sample shows how to create and use watermarks in Docotic.Pdf library.

Please use PdfDocument.CreateXObject() method to add a PdfXObject to your document. This object will be used as a watermark. After a PdfXObject is added to your document you can setup its appearance using methods and properties of the object's canvas (check PdfXObject.Canvas property).

You can add watermark to a page using methods from PdfCanvas.DrawXObject group. The same watermark can be applied to any number of pages.

If you want to add background to a page then set DrawOnBackground property of created PdfXObject to true before using a PdfCanvas.DrawXObject method.