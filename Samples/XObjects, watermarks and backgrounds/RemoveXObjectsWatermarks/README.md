# Remove XObjects from PDF in C# and VB.NET

This sample shows how to remove XObjects, often used for watermarks and backgrounds, from PDF document pages.

## Description

[Docotic.Pdf library](https://bitmiracle.com/pdf-library/) provides access to the collection of page XObjects through the [PdfPage.XObjects](https://api.docotic.com/pdfpage-xobjects) property. The value of the property is a [PdfList](https://api.docotic.com/pdflist(t)) object. 

You can remove any XObject you no longer need using the [PdfList.Remove](https://api.docotic.com/pdflist(t)-remove) or [PdfList.RemoveAt](https://api.docotic.com/pdflist(t)-removeat) method. The [PdfList.Clear](https://api.docotic.com/pdflist(t)-clear) method removes all such objects.

Please note that XObjects are not always watermarks or backgrounds. A PDF file can use such objects for logos and other repeatable items, too.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)