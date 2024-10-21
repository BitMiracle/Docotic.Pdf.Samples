# Customize text appearance in PDF documents in C# and VB.NET
This sample shows how to customize text appearance in PDF documents using [Docotic.Pdf library](https://bitmiracle.com/pdf-library/).

You can use [Document.TextStyleWithFont methods](https://api.docotic.com/layout/document-textstylewithfont) to create a text style for a custom font.
Or you can start with the [TextStyle.Parent](https://api.docotic.com/layout/textstyle-parent) to customize some settings of a parent or default text style.

Use [TextStyle methods](https://api.docotic.com/layout/textstyle#methods) to get the desired text appearance. You can change font size, color, letter spacing, and so on.

Layout elements allow you to apply text style using these methods:
* [PageLayout.TextStyle](https://api.docotic.com/layout/pagelayout-textstyle)
* [LayoutContainer.TextStyle](https://api.docotic.com/layout/layoutcontainer-textstyle)
* [TextContainer.Style](https://api.docotic.com/layout/textcontainer-style)
* [TextSpan.Style](https://api.docotic.com/layout/textspan-style)
* [TextPageNumber.Style](https://api.docotic.com/layout/textpagenumber-style)

This sample applies the `root` style to all page elements. This style applies the red font color and inherits
other settings from library defaults. [Find the default values](https://api.docotic.com/layout/textstyle-parent) in the documentation for the `TextStyle.Parent` property.

The text in the page header inherits `root` style from the page layout settings. However, the code changes the style of the the "superscript" word.

The page content contains a text container that uses the `child` style. This style inherits the font size
from the `root` style, overrides the font color to blue and sets a few other style properties.

The "Child style" line in the text container uses this `child` style. And the "Custom style" line uses
the `custom` style. This style uses custom font with explicitly set additional properties.

This sample code uses free [Docotic.Pdf.Layout add-on](https://www.nuget.org/packages/BitMiracle.Docotic.Pdf.Layout/) for Docotic.Pdf library.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [C# PDF generation quick start guide](https://bitmiracle.com/pdf-library/layout/getting-started)
* [Create PDF documents in C# and VB.NET](https://bitmiracle.com/pdf-library/create-pdf)
* [Fonts](/Samples/Layout/Fonts)
* [Typography](/Samples/Layout/Typography)