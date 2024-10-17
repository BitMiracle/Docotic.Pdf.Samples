# Text markup annotations

This sample shows how to add text markup annotations (highlight, strikeout, underline) to your PDF document in C# and VB.NET.

## Description

Use the [PdfPage.AddHighlightAnnotation](https://api.docotic.com/pdfpage-addhighlightannotation) method to highlight and annotate a page area.

To strike out or underline a page area, use [PdfPage.AddStrikeoutAnnotation](https://api.docotic.com/pdfpage-addstrikeoutannotation), [PdfPage.AddJaggedUnderlineAnnotation](https://api.docotic.com/pdfpage-addjaggedunderlineannotation) and [PdfPage.AddUnderlineAnnotation](https://api.docotic.com/pdfpage-addunderlineannotation) methods.

All these methods create an annotation and return an instance of a [PdfTextMarkupAnnotation](https://api.docotic.com/pdftextmarkupannotation) subclass. You can call the [SetTextBounds](https://api.docotic.com/pdftextmarkupannotation-settextbounds) method to change the annotated area. It is possible to change the [Contents](https://api.docotic.com/pdftextmarkupannotation-contents) and the [Color](https://api.docotic.com/pdftextmarkupannotation-color) properties, too.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Annotations](https://bitmiracle.com/pdf-library/edit/#annotations)
* [Add text annotations to PDF in C# and VB.NET](/Samples/Forms%20and%20Annotations/TextAnnotations) sample