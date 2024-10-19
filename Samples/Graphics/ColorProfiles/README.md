# Using color profiles in C# and VB.NET

This sample shows how to load an ICC profile in a PDF document and then use this color profile to specify a color.

## Description

Besides device-dependent colors, [Docotic.Pdf library](https://bitmiracle.com/pdf-library/) can use colors with associated color profiles. To use color profiles, first add a color profile to your document using one of the [PdfDocument.AddColorProfile](https://api.docotic.com/pdfdocument-addcolorprofile) methods and then use the returned [PdfColorProfile](https://api.docotic.com/pdfcolorprofile) object as a parameter when creating a color.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Setting canvas colors in C# and VB.NET](/Samples/Graphics/Colors) sample
* [Using semi-transparent colors in C# and VB.NET](/Samples/Graphics/Transparency) sample