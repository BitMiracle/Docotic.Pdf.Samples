# Read combo box items in C# and VB.NET

This sample shows how to read items from combo boxes in PDF documents using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

The [PdfComboBox](https://api.docotic.com/pdfcombobox) class derives from the [PdfWidget](https://api.docotic.com/pdfwidget) class. You can access all widgets associated with a page by enumerating the collection of `PdfWidget` objects returned by the [PdfPage.Widgets](https://api.docotic.com/pdfpage-widgets) property.

Check whether each widget is a `PdfComboBox` instance to identify combo boxes. Then use the [PdfComboBox.Items](https://api.docotic.com/pdfcombobox-items) property to read their items.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)