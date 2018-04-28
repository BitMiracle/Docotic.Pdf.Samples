# Save PDF or its page as bitonal TIFF
This sample shows how to save PDF page or the whole PDF document as bitonal (black and white) TIFF image.

To produce a bitonal TIFF create drawing options with specific image compression options. After that use that drawing options with PdfDocument.SaveAsTiff method or PdfPage.Save method.

To create PdfDrawOptions use PdfDrawOptions.Create() method. After that use ImageCompressionOptions.CreateBitonalTiff() to create compression options for bitonal TIFF output.
