# Draw text from the baseline in C# and VB.NET

This sample shows how to draw text starting at font's baseline.

## Description

[PdfCanvas.DrawString](https://api.docotic.com/pdfcanvas-drawstring) methods draw text starting at the left-top corner of the text rectangle. To draw text starting at font's baseline, calculate the distance between the baseline and the top coordinate of the text rectangle.

The [PdfFont.TopSideBearing](https://api.docotic.com/pdffont-topsidebearing) property retrieves the distance between the baseline and the top of the font's bounding box. `TopSideBearing` expressed in the glyph coordinate system. Use the value of the [PdfFont.TransformationMatrix](https://api.docotic.com/pdffont-transformationmatrix) property to map `TopSideBearing` to the canvas coordinate system.

For all PDF fonts except some tricky Type3 fonts, `TransformationMatrix` has the following structure: `{ M11, 0, 0, M22, 0, 0 }`. For such fonts, map `TopSideBearing` to the canvas coordinate system using this formula: `font.TopSideBearing * font.TransformationMatrix.M22 * canvas.FontSize`.

With `TopSideBearing`, expressed in the canvas coordinate system, you can draw text using a `DrawString` method. Convert the baseline coordinate to the top coordinate of the text rectangle using this formula: `top = (baselineY - topSideBearingCanvasSpace)`. Finally, use calculated top coordinate for a `DrawString` call to draw text starting at the baseline position.

## See also
* [Get free time-limited license key](https://bitmiracle.com/pdf-library/download)
* [Draw text on PDF canvas in C# and VB.NET](/Samples/Text/DrawText)