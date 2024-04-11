# Typography in Docotic.Pdf.Layout add-on
This sample shows how to manage text styles using Typography class from [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/).

[Typography class](https://bitmiracle.com/pdf-library/api/layout/typography) allows you to define and use text styles without keeping intermediate variables.
The library automatically applies some predefined styles (`Document`, `Header`, `Body`, `Footer`, `Hyperlink`) to corresponding document parts.

Use [Document.Typography methods](https://bitmiracle.com/pdf-library/api/layout/document-typography)
to modify standard styles or provide custom `Typography` implementation.
Later, use `Typography` object in [PageLayout.TextStyle(Func<Typography, TextStyle>)](https://bitmiracle.com/pdf-library/api/layout/pagelayout-textstyle#func_typography_textstyle__),
[LayoutContainer.TextStyle(Func<Typography, TextStyle>)](https://bitmiracle.com/pdf-library/api/layout/layoutcontainer-textstyle#func_typography_textstyle__),
[TextContainer.Style(Func<Typography, TextStyle>)](https://bitmiracle.com/pdf-library/api/layout/textcontainer-style#func_typography_textstyle__),
and other similar methods.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [C# PDF generation quick start guide](https://bitmiracle.com/pdf-library/layout/getting-started)
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf) article
* [Fonts](/Samples/Layout/Fonts)
* [Text styling](/Samples/Layout/TextStyling)
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)