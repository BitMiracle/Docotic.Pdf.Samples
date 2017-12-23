# Color profiles
This sample shows how to load an ICC profile and then create and use a color with that color profile.

In addition to device-dependent colors Docotic.Pdf library can also use colors with associated color profiles. To use color profiles, first add a color profile to your document using PdfDocument.AddColorProfile method and then use the returned PdfColorProfile object as a parameter when creating a color.
