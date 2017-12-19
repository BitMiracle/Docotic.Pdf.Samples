# Text markup annotations
This sample shows how to add text markup annotations (highlight, strikeout, underline) to your PDF document.

Use PdfPage.AddHighlightAnnotation method to highlight and annotate a page area. This methods return an instance of the created annotation.

Alternatively, use PdfPage.AddStrikeoutAnnotation, PdfPage.AddJaggedUnderlineAnnotation and PdfPage.AddUnderlineAnnotation methods to strike out or underline a page area.

You can also modify new or existing text markup annotations. Cast corresponding widgets to PdfTextMarkupAnnotation class. Then call SetTextBounds() method or change Contents and Color properties of PdfTextMarkupAnnotation class.