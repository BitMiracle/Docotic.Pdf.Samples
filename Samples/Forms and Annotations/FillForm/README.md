# Fill PDF forms in C# and VB.NET
This sample shows how to fill forms in existing PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

To change form fields in a PDF, open the document and enumerate its controls using the [PdfDocument.GetControls](https://api.docotic.com/pdfdocument-getcontrols) method. Depending on the control [Name](https://api.docotic.com/pdfcontrol-name), provide the value.

If you only need to change a field or two, use the [PdfDocument.GetControl](https://api.docotic.com/pdfdocument-getcontrol) method to find the controls by their full or partial names.

You can also import a file in the FDF format with all field values and fill the form in one take.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [How to fill in a PDF form](https://bitmiracle.com/pdf-library/edit/#fill-form)
* [Forms Data Format (FDF)](https://bitmiracle.com/pdf-library/edit/#fdf)
* [Find PDF control by name](/Samples/Forms%20and%20Annotations/FindControlByName) sample