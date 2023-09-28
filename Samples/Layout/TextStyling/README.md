# Customize text appearance in PDF documents in C# and VB.NET
This sample shows how to customize text appearance in PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

You can use [Document.TextStyleWithFont methods](https://bitmiracle.com/pdf-library/api/layout/document-textstylewithfont) to create a text style for a custom font.
Or you can start with [TextStyle.Parent](https://bitmiracle.com/pdf-library/api/layout/textstyle-parent) to customize some settings of a parent or default text style.

Use [TextStyle methods](https://bitmiracle.com/pdf-library/api/layout/textstyle#methods) to reach a desired text appearance. You can change font size, color, letter spacing, and so on.

Layout elements allow you to apply text style using these methods:
* [PageLayout.TextStyle](https://bitmiracle.com/pdf-library/api/layout/pagelayout-textstyle)
* [LayoutContainer.TextStyle](https://bitmiracle.com/pdf-library/api/layout/layoutcontainer-textstyle)
* [TextContainer.Style](https://bitmiracle.com/pdf-library/api/layout/textcontainer-style)
* [TextSpan.Style](https://bitmiracle.com/pdf-library/api/layout/textspan-style)
* [TextPageNumber.Style](https://bitmiracle.com/pdf-library/api/layout/textpagenumber-style)

This sample applies the `root` style to all page elements. This style applies the red font color and inherits
other settings from library defaults. Default values are documented in the documentation for
the `TextStyle.Parent` property.

The text in the page header inherits `root` style from the page layout settings. However, the style of
the "superscript" word is customized.

The page content contains a text container that uses the `child` style. This style inherits the font size
from the `root` style, overrides the font color to blue and sets a few other style properties.

The "Child style" line in the text container uses this `child` style. And the "Custom style" line uses
the `custom` style. This style uses custom font with explicitly set additional properties.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf.aspx) article
* [Fonts](/Samples/Layout/Fonts)
* [Typography](/Samples/Layout/Typography)
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download-pdf-library.aspx)