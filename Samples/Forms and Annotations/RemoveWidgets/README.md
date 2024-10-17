# Remove widgets from PDF in C# and VB.NET

This sample shows how to remove widgets from your PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

## Description

There are two ways to remove widgets. You can use the [PdfPage.Widgets](https://api.docotic.com/pdfpage-widgets) property to access the collection of a page widgets. Then use the [Remove](https://api.docotic.com/pdfwidgetcollection-remove), [RemoveAt](https://api.docotic.com/pdfwidgetcollection-removeat), and [Clear](https://api.docotic.com/pdfwidgetcollection-clear) methods of the [PdfWidgetCollection](https://api.docotic.com/pdfwidgetcollection) class to remove the widgets you don’t need.

Another way is to enumerate the collection of document widgets using the [PdfDocument.GetWidgets](https://api.docotic.com/pdfdocument-getwidgets) method. Then use the [PdfDocument.RemoveWidget](https://api.docotic.com/pdfdocument-removewidget) method to remove unwanted widgets.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Remove annotations](https://bitmiracle.com/pdf-library/edit/#remove-annotations)
* [Flatten form fields in C# and VB.NET](/Samples/Forms%20and%20Annotations/FlattenFormFields) sample