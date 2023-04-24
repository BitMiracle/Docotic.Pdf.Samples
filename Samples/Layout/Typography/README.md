# Typography in Docotic.Pdf.Layout add-on
This sample shows how to manage text styles using Typography class from [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/).

Typography class allows you to define and use text styles without keeping intermediate variables.
The library automatically applies some predefined styles (Document, Header, Body, Footer, Hyperlink) to corresponding document parts.

Use Document.Typography method to modify standard styles or provide custom Typography implementation.
Later, use Typography object in PageLayout.TextStyle(Func<Typography, TextStyle>),
LayoutContainer.TextStyle(Func<Typography, TextStyle>), TextContainer.TextStyle(Func<Typography, TextStyle>),
and other similar methods.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Fonts](/Samples/Layout/Fonts)
* [Text styling](/Samples/Layout/TextStyling)
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)