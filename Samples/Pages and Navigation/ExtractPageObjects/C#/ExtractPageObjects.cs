using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractPageObjects
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            const string PathToFile = "ExtractPageObjects.png";

            using (var pdf = new PdfDocument(@"Sample Data\gmail-cheat-sheet.pdf"))
            {
                PdfPage page = pdf.Pages[0];

                const float TargetResolution = 300;
                double scaleFactor = TargetResolution / page.Canvas.Resolution;
                using (Bitmap bitmap = new Bitmap((int)(page.Width * scaleFactor), (int)(page.Height * scaleFactor)))
                {
                    bitmap.SetResolution(TargetResolution, TargetResolution);

                    using (var gr = Graphics.FromImage(bitmap))
                    {
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.PageUnit = GraphicsUnit.Point;

                        gr.SetClip(page.CropBox.ToRectangleF(page.Height), CombineMode.Intersect);

                        foreach (PdfPageObject obj in page.GetObjects())
                        {
                            switch (obj.Type)
                            {
                                case PdfPageObjectType.Text:
                                    drawText(gr, (PdfTextData)obj);
                                    break;

                                case PdfPageObjectType.Image:
                                    drawImage(gr, (PdfPaintedImage)obj);
                                    break;

                                case PdfPageObjectType.Path:
                                    drawPath(gr, (PdfPath)obj);
                                    break;
                            }
                        }
                    }

                    bitmap.Save(PathToFile, ImageFormat.Png);
                }
            }

            Process.Start(PathToFile);
        }

        private static void drawText(Graphics gr, PdfTextData td)
        {
            if (td.RenderingMode == PdfTextRenderingMode.NeitherFillNorStroke ||
                td.RenderingMode == PdfTextRenderingMode.AddToPath)
                return;

            if (Math.Abs(td.FontSize) < 0.001)
                return;

            if (Math.Abs(td.Bounds.Width) < 0.001 || Math.Abs(td.Bounds.Height) < 0.001)
                return;

            saveStateAndDraw(gr, td.ClipRegion, () =>
            {
                using (Font font = toGdiFont(td.Font, td.FontSize))
                {
                    using (Brush brush = toGdiBrush(td.Brush))
                    {
                        gr.TranslateTransform((float)td.Position.X, (float)td.Position.Y);
                        concatMatrix(gr, td.TransformationMatrix);

                        gr.DrawString(td.Text, font, brush, PointF.Empty);
                    }
                }
            });
        }

        private static void drawImage(Graphics gr, PdfPaintedImage image)
        {
            // Do not render images with zero width or height. GDI+ throws OutOfMemoryException
            // in x86 processes for regular image size (e.g. 1x51) and very small transformation
            // matrix (e.g. { 0.000005, 0, 0, 0.003, x, y }).
            PdfSize paintedImageSize = image.Bounds.Size;
            if (Math.Abs(paintedImageSize.Width) < 0.001 || Math.Abs(paintedImageSize.Height) < 0.001)
                return;

            // ignore mask images in this sample
            if (image.Image.IsMask)
                return;

            using (var stream = new MemoryStream())
            {
                image.Image.Save(stream);
                using (Bitmap bitmap = (Bitmap)Image.FromStream(stream))
                {
                    saveStateAndDraw(gr, image.ClipRegion, () =>
                    {
                        gr.TranslateTransform((float)image.Position.X, (float)image.Position.Y);
                        concatMatrix(gr, image.TransformationMatrix);

                        // Important for rendering of neighbour image tiles
                        gr.PixelOffsetMode = PixelOffsetMode.Half;

                        PdfSize imageSize = new PdfSize(image.Image.Width, image.Image.Height);
                        if (imageSize.Width < bitmap.Width && imageSize.Height < bitmap.Height)
                        {
                            // the bitmap produced from the image is larger than the image.
                            // usually this happens when image has a mask image which is larger than the image itself.
                            InterpolationMode current = gr.InterpolationMode;
                            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            gr.DrawImage(bitmap, 0, 0, (float)imageSize.Width, (float)imageSize.Height);
                            gr.InterpolationMode = current;
                        }
                        else
                        {
                            // bitmap has the same size 
                            // or one of it's dimensions is longer than the corresponding image dimension
                            gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                        }
                    });
                }
            }
        }

        private static void drawPath(Graphics gr, PdfPath path)
        {
            if (!path.PaintMode.HasValue)
                return;

            saveStateAndDraw(gr, path.ClipRegion, () =>
            {
                concatMatrix(gr, path.TransformationMatrix);

                using (var gdiPath = new GraphicsPath())
                {
                    toGdiPath(path, gdiPath);

                    fillStrokePath(gr, path, gdiPath);
                }
            });
        }

        private static void saveStateAndDraw(Graphics gr, PdfClipRegion clipRegion, Action draw)
        {
            var state = gr.Save();
            try
            {
                setClipRegion(gr, clipRegion);

                draw();
            }
            finally
            {
                gr.Restore(state);
            }
        }

        private static void setClipRegion(Graphics gr, PdfClipRegion clipRegion)
        {
            if (clipRegion.IntersectedPaths.Count == 0)
                return;

            Matrix transformationBefore = gr.Transform;
            try
            {
                gr.Transform = new Matrix();
                foreach (PdfPath clipPath in clipRegion.IntersectedPaths)
                {
                    using (var gdiPath = new GraphicsPath())
                    {
                        toGdiPath(clipPath, gdiPath);
                        gdiPath.Transform(toGdiMatrix(clipPath.TransformationMatrix));

                        gdiPath.FillMode = (FillMode)clipPath.ClipMode.Value;
                        gr.SetClip(gdiPath, CombineMode.Intersect);
                    }
                }
            }
            finally
            {
                gr.Transform = transformationBefore;
            }
        }

        private static void toGdiPath(PdfPath path, GraphicsPath gdiPath)
        {
            foreach (PdfSubpath subpath in path.Subpaths)
            {
                gdiPath.StartFigure();

                foreach (PdfPathSegment segment in subpath.Segments)
                {
                    switch (segment.Type)
                    {
                        case PdfPathSegmentType.Point:
                            // A singlepoint open subpath produces no output.
                            break;

                        case PdfPathSegmentType.Line:
                            PdfLineSegment line = (PdfLineSegment)segment;
                            gdiPath.AddLine(line.Start.ToPointF(), line.End.ToPointF());
                            break;

                        case PdfPathSegmentType.Bezier:
                            PdfBezierSegment bezier = (PdfBezierSegment)segment;
                            gdiPath.AddBezier(bezier.Start.ToPointF(),
                                bezier.FirstControl.ToPointF(),
                                bezier.SecondControl.ToPointF(),
                                bezier.End.ToPointF()
                            );
                            break;

                        case PdfPathSegmentType.Rectangle:
                            RectangleF rect = ((PdfRectangleSegment)segment).Bounds.ToRectangleF();

                            // GDI+ does not render rectangles with negative or very small width and height. Render such
                            // rectangles by lines, but respect direction. Otherwise non-zero winding rule for
                            // path filling will not work.
                            gdiPath.AddLines(new PointF[]
                            {
                                rect.Location,
                                new PointF(rect.X + rect.Width, rect.Y),
                                new PointF(rect.X + rect.Width, rect.Y + rect.Height),
                                new PointF(rect.X, rect.Y + rect.Height),
                                rect.Location
                            });
                            break;

                        case PdfPathSegmentType.CloseSubpath:
                            gdiPath.CloseFigure();
                            break;
                    }
                }
            }
        }

        private static void fillStrokePath(Graphics gr, PdfPath path, GraphicsPath gdiPath)
        {
            PdfDrawMode paintMode = path.PaintMode.Value;
            if (paintMode == PdfDrawMode.Fill || paintMode == PdfDrawMode.FillAndStroke)
            {
                using (var brush = toGdiBrush(path.Brush))
                {
                    gdiPath.FillMode = (FillMode)path.FillMode.Value;
                    gr.FillPath(brush, gdiPath);
                }
            }

            if (paintMode == PdfDrawMode.Stroke || paintMode == PdfDrawMode.FillAndStroke)
            {
                using (var pen = toGdiPen(path.Pen))
                {
                    gr.DrawPath(pen, gdiPath);
                }
            }
        }

        private static void concatMatrix(Graphics gr, PdfMatrix transformation)
        {
            using (var m = toGdiMatrix(transformation))
            {
                using (Matrix current = gr.Transform)
                {
                    current.Multiply(m, MatrixOrder.Prepend);
                    gr.Transform = current;
                }
            }
        }

        private static Matrix toGdiMatrix(PdfMatrix matrix)
        {
            return new Matrix(
                (float)matrix.M11,
                (float)matrix.M12,
                (float)matrix.M21,
                (float)matrix.M22,
                (float)matrix.OffsetX,
                (float)matrix.OffsetY
            );
        }

        private static Brush toGdiBrush(PdfBrushInfo brush)
        {
            return new SolidBrush(toGdiColor(brush.Color, brush.Opacity));
        }

        private static Pen toGdiPen(PdfPenInfo pen)
        {
            return new Pen(toGdiColor(pen.Color, pen.Opacity), (float)pen.Width)
            {
                LineJoin = toGdiLineJoin(pen.LineJoin),
                EndCap = toGdiLineCap(pen.EndCap),
                MiterLimit = (float)pen.MiterLimit
            };
        }

        private static Color toGdiColor(PdfColor pdfColor, int opacityPercent)
        {
            if (pdfColor == null)
                return Color.Empty;

            // NOTE: PdfColor.ToColor() method is not supported in version for .NET Standard
            Color color = pdfColor.ToColor();

            int alpha = 255;
            if (opacityPercent < 100 && opacityPercent >= 0)
                alpha = (int)(255.0 * opacityPercent / 100.0);

            return Color.FromArgb(alpha, color.R, color.G, color.B);
        }

        private static Font toGdiFont(PdfFont font, double fontSize)
        {
            string fontName = getFontName(font);
            FontStyle fontStyle = getFontStyle(font);

            return new Font(fontName, (float)fontSize, fontStyle);
        }

        private static LineCap toGdiLineCap(PdfLineCap lineCap)
        {
            switch (lineCap)
            {
                case PdfLineCap.ButtEnd:
                    return LineCap.Flat;

                case PdfLineCap.Round:
                    return LineCap.Round;

                case PdfLineCap.ProjectingSquare:
                    return LineCap.Square;

                default:
                    throw new InvalidOperationException("We should never be here");
            }
        }

        private static LineJoin toGdiLineJoin(PdfLineJoin lineJoin)
        {
            switch (lineJoin)
            {
                case PdfLineJoin.Miter:
                    return LineJoin.Miter;

                case PdfLineJoin.Round:
                    return LineJoin.Round;

                case PdfLineJoin.Bevel:
                    return LineJoin.Bevel;

                default:
                    throw new InvalidOperationException("We should never be here");
            }
        }

        private static string getFontName(PdfFont font)
        {
            // A trick to load a similar font for system. Ideally we should load font from raw bytes. Use PdfFont.Save()
            // method for that.
            string fontName = font.Name;
            if (fontName.Contains("Times"))
                return "Times New Roman";

            if (fontName.Contains("Garamond"))
                return "Garamond";

            if (fontName.Contains("Arial") && !fontName.Contains("Unicode"))
                return "Arial";

            return font.Name;
        }

        private static FontStyle getFontStyle(PdfFont font)
        {
            FontStyle fontStyle = FontStyle.Regular;

            if (font.Bold)
                fontStyle |= FontStyle.Bold;

            if (font.Italic)
                fontStyle |= FontStyle.Italic;

            return fontStyle;
        }
    }
}
