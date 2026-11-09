# Remove widgets from PDF in C# and VB.NET

This sample shows how to remove widgets from PDF documents using the [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

There are two ways to remove widgets.

The first approach is to use the [PdfPage.Widgets](https://api.docotic.com/pdfpage-widgets) property to access the collection of widgets associated with a page. You can then use the [PdfWidgetCollection.RemoveAt](https://api.docotic.com/pdfwidgetcollection-removeat) method to remove individual widgets or the [PdfWidgetCollection.Clear](https://api.docotic.com/pdfwidgetcollection-clear) method to remove all widgets from the page.

Alternatively, you can enumerate all widgets in a document using the [PdfDocument.GetWidgets](https://api.docotic.com/pdfdocument-getwidgets) method and call the `PdfWidget.Remove` method on each widget you want to remove.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Remove annotations](https://bitmiracle.com/pdf-library/edit/#remove-annotations)
* [Flatten form fields in C# and VB.NET](/Samples/Forms%20and%20Annotations/FlattenFormFields) sample