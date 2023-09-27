# Flatten form fields in C# and VB.NET
This sample shows how to flatten form fields in a PDF document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

To flatten form fields in a PDF file, please use the [PdfDocument.FlattenControls() method](https://bitmiracle.com/pdf-library/api/pdfdocument-flattencontrols). This method draws each control on its parent page and then removes the control from the document.

Flattening locks form fields from editing and can significantly reduce output file size.

Alternatively, you can:
1. Flatten controls and annotations using the [PdfDocument.FlattenWidgets() method](https://bitmiracle.com/pdf-library/api/pdfdocument-flattenwidgets)
2. Flatten individual controls or annotations using the [PdfWidget.Flatten() method](https://bitmiracle.com/pdf-library/api/pdfwidget-flatten)

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)