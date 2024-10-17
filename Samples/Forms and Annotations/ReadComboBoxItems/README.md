# Read combo box items in C# and VB.NET

This sample shows how to read items of combo boxes in a PDF document using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

The [PdfComboBox](https://api.docotic.com/pdfcombobox) class is a child of the [PdfWidget](https://api.docotic.com/pdfwidget) class. You can access all the widgets associated with a page by enumerating a read-only collection of `PdfWidget` objects. 

Use the [PdfPage.Widgets](https://api.docotic.com/pdfpage-widgets) property to get the collection. Then check if a widget is an instance of the `PdfComboBox` class. You can read combo box items using the [PdfComboBox.Items](https://api.docotic.com/pdfcombobox-items) property.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)