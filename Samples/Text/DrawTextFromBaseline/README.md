# Draw text from baseline
This sample shows how to draw text starting at font's baseline.

PdfCanvas.DrawString method draws string starting at left-top corner of the text rectangle. To draw text starting at font's baseline, calculate the distance between the baseline and the top coordinate of the text rectangle.

PdfFont.TopSideBearing property retrieves the distance between the baseline and the top of the font's bounding box. TopSideBearing expressed in the glyph coordinate system. Use PdfFont.TransformationMatrix to map TopSideBearing to the canvas coordinate system.

For all PDF fonts except some tricky Type3 fonts, TransformationMatrix has the following structure: { M11, 0, 0, M22, 0, 0 }. For such fonts, map TopSideBearing to the canvas coordinate system by this formula: font.TopSideBearing * font.TransformationMatrix.M22 * canvas.FontSize.

With TopSideBearing, expressed in the canvas coordinate system, you can draw text using DrawString method. Convert baseline coordinate to the top coordinate of the text rectangle by this formula: top = (baselineY - topSideBearingCanvasSpace). Finally, pass calculated top coordinate to DrawString method to draw text starting at the baseline position.