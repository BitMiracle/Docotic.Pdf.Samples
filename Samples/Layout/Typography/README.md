# Typography in Docotic.Pdf.Layout add-on
This sample shows how to manage text styles using the `Typography` class from [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/).

The [Typography class](https://api.docotic.com/layout/typography) allows you to define and use text styles without keeping intermediate variables.
The library automatically applies some predefined styles (`Document`, `Header`, `Body`, `Footer`, `Hyperlink`) to the corresponding document parts.

Use [Document.Typography methods](https://api.docotic.com/layout/document-typography)
to modify standard styles or provide a custom `Typography` implementation.
Later, use the `Typography` object in the [PageLayout.TextStyle(Func<Typography, TextStyle>)](https://api.docotic.com/layout/pagelayout-textstyle#func_typography_textstyle__),
[LayoutContainer.TextStyle(Func<Typography, TextStyle>)](https://api.docotic.com/layout/layoutcontainer-textstyle#func_typography_textstyle__),
[TextContainer.Style(Func<Typography, TextStyle>)](https://api.docotic.com/layout/textcontainer-style#func_typography_textstyle__),
and other similar methods.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [C# PDF generation quick start guide](https://bitmiracle.com/pdf-library/layout/getting-started)
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf)
* [Fonts](/Samples/Layout/Fonts)
* [Text styling](/Samples/Layout/TextStyling)