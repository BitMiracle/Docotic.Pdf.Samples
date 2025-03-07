using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Versioning;
using BitMiracle.Docotic.Pdf.Gdi;

namespace BitMiracle.Docotic.Pdf.Samples
{
    [SupportedOSPlatform("windows")]
    public static class ExtractPageObjects
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            const string PathToFile = "ExtractPageObjects.png";

            using (var pdf = new PdfDocument(@"..\Sample Data\gmail-cheat-sheet.pdf"))
            {
                PdfPage page = pdf.Pages[0];

                const float TargetResolution = 300;
                double scaleFactor = TargetResolution / page.Resolution;
                using var bitmap = new Bitmap((int)(page.Width * scaleFactor), (int)(page.Height * scaleFactor));
                bitmap.SetResolution(TargetResolution, TargetResolution);

                using (var gr = Graphics.FromImage(bitmap))
                {
                    gr.SmoothingMode = SmoothingMode.HighQuality;
                    gr.PageUnit = GraphicsUnit.Point;

                    float userUnit = (float)page.UserUnit;
                    gr.ScaleTransform(userUnit, userUnit);

                    gr.SetClip(page.CropBox.ToRectangleF(page.Height), CombineMode.Intersect);

                    // We do not need to handle marked content and XObjects in this sample.
                    // You may want to set these options to false in order to:
                    // * Respect transparency groups for XObjects
                    // * Respect layers or tagged content
                    //
                    // Look at the EditPageContent sample that shows how to handle marked content and XObjects.
                    var options = new PdfObjectExtractionOptions
                    {
                        FlattenMarkedContent = true,
                        FlattenXObjects = true,
                    };
                    foreach (PdfPageObject obj in page.GetObjects(options))
                    {
                        switch (obj.Type)
                        {
                            case PdfPageObjectType.Text:
                                DrawText(gr, (PdfTextData)obj, userUnit);
                                break;

                            case PdfPageObjectType.Image:
                                DrawImage(gr, (PdfPaintedImage)obj, userUnit);
                                break;

                            case PdfPageObjectType.Path:
                                DrawPath(gr, (PdfPath)obj, userUnit);
                                break;
                        }
                    }
                }

                bitmap.Save(PathToFile, ImageFormat.Png);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }

        private static void DrawText(Graphics gr, PdfTextData td, float userUnit)
        {
            if (td.RenderingMode == PdfTextRenderingMode.NeitherFillNorStroke ||
                td.RenderingMode == PdfTextRenderingMode.AddToPath)
                return;

            double fontSizeAbs = Math.Abs(td.FontSize);
            if (fontSizeAbs < 0.001)
                return;

            if (Math.Abs(td.Bounds.Width) < 0.001 || Math.Abs(td.Bounds.Height) < 0.001)
                return;

            SaveStateAndDraw(gr, td.ClipRegion, userUnit, () =>
            {
                gr.TranslateTransform((float)td.Position.X, (float)td.Position.Y);
                ConcatMatrix(gr, td.TransformationMatrix);
                if (Math.Sign(td.FontSize) < 0)
                    gr.ScaleTransform(1, -1);

                using Font font = ToGdiFont(td.Font, fontSizeAbs);
                using Brush brush = ToGdiBrush(td.Brush);
                gr.DrawString(td.GetText(), font, brush, PointF.Empty);
            });
        }

        private static void DrawImage(Graphics gr, PdfPaintedImage image, float userUnit)
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

            using var stream = new MemoryStream();
            image.Image.Save(stream);

            using Bitmap bitmap = (Bitmap)Image.FromStream(stream);
            SaveStateAndDraw(gr, image.ClipRegion, userUnit, () =>
            {
                gr.TranslateTransform((float)image.Position.X, (float)image.Position.Y);
                ConcatMatrix(gr, image.TransformationMatrix);

                // Important for rendering of neighbour image tiles
                gr.PixelOffsetMode = PixelOffsetMode.Half;

                var imageSize = new PdfSize(image.Image.Width, image.Image.Height);
                if (imageSize.Width < bitmap.Width && imageSize.Height < bitmap.Height)
                {
                    // the bitmap produced from the image is larger than the image.
                    // usually this happens when an image has a mask image which is larger than the image itself.
                    InterpolationMode current = gr.InterpolationMode;
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.DrawImage(bitmap, 0, 0, (float)imageSize.Width, (float)imageSize.Height);
                    gr.InterpolationMode = current;
                }
                else
                {
                    // bitmap has the same size,
                    // or one of its dimensions is longer than the corresponding image dimension
                    gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                }
            });
        }

        private static void DrawPath(Graphics gr, PdfPath path, float userUnit)
        {
            if (!path.PaintMode.HasValue)
                return;

            SaveStateAndDraw(gr, path.ClipRegion, userUnit, () =>
            {
                ConcatMatrix(gr, path.TransformationMatrix);

                using var gdiPath = new GraphicsPath();
                ToGdiPath(path, gdiPath);
                FillStrokePath(gr, path, gdiPath);
            });
        }

        private static void SaveStateAndDraw(Graphics gr, PdfClipRegion clipRegion, float userUnit, Action draw)
        {
            var state = gr.Save();
            try
            {
                SetClipRegion(gr, clipRegion, userUnit);

                draw();
            }
            finally
            {
                gr.Restore(state);
            }
        }

        private static void SetClipRegion(Graphics gr, PdfClipRegion clipRegion, float userUnit)
        {
            if (clipRegion.IntersectedPaths.Count == 0)
                return;

            Matrix transformationBefore = gr.Transform;
            try
            {
                gr.Transform = new Matrix(userUnit, 0, 0, userUnit, 0, 0);
                foreach (PdfPath clipPath in clipRegion.IntersectedPaths)
                {
                    using var gdiPath = new GraphicsPath();
                    ToGdiPath(clipPath, gdiPath);
                    gdiPath.Transform(ToGdiMatrix(clipPath.TransformationMatrix));

                    gdiPath.FillMode = (FillMode)clipPath.ClipMode!.Value;
                    gr.SetClip(gdiPath, CombineMode.Intersect);
                }
            }
            finally
            {
                gr.Transform = transformationBefore;
            }
        }

        private static void ToGdiPath(PdfPath path, GraphicsPath gdiPath)
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

                            // GDI+ does not render rectangles with a negative or
                            // very small width and height. Render such rectangles by lines,
                            // but respect direction. Otherwise, non-zero winding rule for
                            // path filling will not work.
                            gdiPath.AddLines(new[]
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

        private static void FillStrokePath(Graphics gr, PdfPath path, GraphicsPath gdiPath)
        {
            PdfDrawMode paintMode = path.PaintMode!.Value;
            if (paintMode == PdfDrawMode.Fill || paintMode == PdfDrawMode.FillAndStroke)
            {
                using var brush = ToGdiBrush(path.Brush);
                gdiPath.FillMode = (FillMode)path.FillMode!.Value;
                gr.FillPath(brush, gdiPath);
            }

            if (paintMode == PdfDrawMode.Stroke || paintMode == PdfDrawMode.FillAndStroke)
            {
                using var pen = ToGdiPen(path.Pen);
                gr.DrawPath(pen, gdiPath);
            }
        }

        private static void ConcatMatrix(Graphics gr, PdfMatrix transformation)
        {
            using var m = ToGdiMatrix(transformation);
            using Matrix current = gr.Transform;
            current.Multiply(m, MatrixOrder.Prepend);
            gr.Transform = current;
        }

        private static Matrix ToGdiMatrix(PdfMatrix matrix)
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

        private static Brush ToGdiBrush(PdfBrushInfo brush)
        {
            return new SolidBrush(ToGdiColor(brush.Color, brush.Opacity));
        }

        private static Pen ToGdiPen(PdfPenInfo pen)
        {
            return new Pen(ToGdiColor(pen.Color, pen.Opacity), (float)pen.Width)
            {
                LineJoin = ToGdiLineJoin(pen.LineJoin),
                EndCap = ToGdiLineCap(pen.EndCap),
                MiterLimit = (float)pen.MiterLimit
            };
        }

        private static Color ToGdiColor(PdfColor? pdfColor, int opacityPercent)
        {
            if (pdfColor == null)
                return Color.Empty;

            PdfRgbColor rgbColor = pdfColor.ToRgb();

            int alpha = 255;
            if (opacityPercent < 100 && opacityPercent >= 0)
                alpha = (int)(255.0 * opacityPercent / 100.0);

            return Color.FromArgb(alpha, rgbColor.R, rgbColor.G, rgbColor.B);
        }

        private static Font ToGdiFont(PdfFont font, double fontSize)
        {
            string fontName = GetFontName(font);
            FontStyle fontStyle = GetFontStyle(font);

            return new Font(fontName, (float)fontSize, fontStyle);
        }

        private static LineCap ToGdiLineCap(PdfLineCap lineCap)
        {
            return lineCap switch
            {
                PdfLineCap.ButtEnd => LineCap.Flat,
                PdfLineCap.Round => LineCap.Round,
                PdfLineCap.ProjectingSquare => LineCap.Square,
                _ => throw new InvalidOperationException("We should never be here"),
            };
        }

        private static LineJoin ToGdiLineJoin(PdfLineJoin lineJoin)
        {
            return lineJoin switch
            {
                PdfLineJoin.Miter => LineJoin.Miter,
                PdfLineJoin.Round => LineJoin.Round,
                PdfLineJoin.Bevel => LineJoin.Bevel,
                _ => throw new InvalidOperationException("We should never be here"),
            };
        }

        private static string GetFontName(PdfFont font)
        {
            // A trick to load a similar font from the system. Ideally, we should
            // load font from raw bytes. Use PdfFont.Save methods to produce the bytes.
            string fontName = font.Name;
            if (fontName.Contains("Courier"))
                return "Courier New";

            if (fontName.Contains("Times"))
                return "Times New Roman";

            if (fontName.Contains("Garamond"))
                return "Garamond";

            if (fontName.Contains("Arial") && !fontName.Contains("Unicode"))
                return "Arial";

            return font.Name;
        }

        private static FontStyle GetFontStyle(PdfFont font)
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
