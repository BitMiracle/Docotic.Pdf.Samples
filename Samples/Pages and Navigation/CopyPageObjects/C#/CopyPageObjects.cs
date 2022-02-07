using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CopyPageObjects
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            const string PathToFile = "CopyPageObjects.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\BRAILLE CODES WITH TRANSLATION.pdf"))
            {
                using (PdfDocument copy = pdf.CopyPages(0, 1))
                {
                    PdfPage sourcePage = copy.Pages[0];
                    PdfPage copyPage = copy.AddPage();

                    copyPage.UserUnit = sourcePage.UserUnit;
                    copyPage.Rotation = sourcePage.Rotation;
                    copyPage.MediaBox = sourcePage.MediaBox;
                    if (sourcePage.CropBox != sourcePage.MediaBox)
                        copyPage.CropBox = sourcePage.CropBox;

                    PdfCanvas target = copyPage.Canvas;
                    foreach (PdfPageObject obj in sourcePage.GetObjects())
                    {
                        target.SaveState();
                        setClipRegion(target, obj.ClipRegion);

                        if (obj.Type == PdfPageObjectType.Path)
                        {
                            PdfPath path = (PdfPath)obj;
                            target.Transform(path.TransformationMatrix);

                            if (path.PaintMode == PdfDrawMode.Fill || path.PaintMode == PdfDrawMode.FillAndStroke)
                                setBrush(target.Brush, path.Brush);

                            if (path.PaintMode == PdfDrawMode.Stroke || path.PaintMode == PdfDrawMode.FillAndStroke)
                                setPen(target.Pen, path.Pen);

                            appendPath(target, path);
                            drawPath(target, path);
                        }
                        else if (obj.Type == PdfPageObjectType.Image)
                        {
                            PdfPaintedImage image = (PdfPaintedImage)obj;
                            target.TranslateTransform(image.Position.X, image.Position.Y);
                            target.Transform(image.TransformationMatrix);

                            setBrush(target.Brush, image.Brush);
                            target.DrawImage(image.Image, 0, 0, 0);
                        }
                        else if (obj.Type == PdfPageObjectType.Text)
                        {
                            PdfTextData text = (PdfTextData)obj;
                            drawText(target, text);
                        }

                        target.RestoreState();
                    }

                    copy.RemovePage(0);

                    copy.Save(PathToFile);
                }
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }

        private static void setClipRegion(PdfCanvas canvas, PdfClipRegion clipRegion)
        {
            if (clipRegion.IntersectedPaths.Count == 0)
                return;

            PdfMatrix transformationBefore = canvas.TransformationMatrix;
            try
            {
                foreach (PdfPath clipPath in clipRegion.IntersectedPaths)
                {
                    canvas.ResetTransform();
                    canvas.Transform(clipPath.TransformationMatrix);
                    appendPath(canvas, clipPath);
                    canvas.SetClip(clipPath.ClipMode.Value);
                }
            }
            finally
            {
                canvas.ResetTransform();
                canvas.Transform(transformationBefore);
            }
        }

        private static void setBrush(PdfBrush dst, PdfBrushInfo src)
        {
            PdfColor color = src.Color;
            if (color != null)
                dst.Color = color;

            dst.Opacity = src.Opacity;

            var pattern = src.Pattern;
            if (pattern != null)
                dst.Pattern = pattern;
        }

        private static void setPen(PdfPen dst, PdfPenInfo src)
        {
            PdfColor color = src.Color;
            if (color != null)
                dst.Color = color;

            var pattern = src.Pattern;
            if (pattern != null)
                dst.Pattern = pattern;

            dst.DashPattern = src.DashPattern;
            dst.EndCap = src.EndCap;
            dst.LineJoin = src.LineJoin;
            dst.MiterLimit = src.MiterLimit;
            dst.Opacity = src.Opacity;
            dst.Width = src.Width;
        }

        private static void appendPath(PdfCanvas target, PdfPath path)
        {
            foreach (PdfSubpath subpath in path.Subpaths)
            {
                foreach (PdfPathSegment segment in subpath.Segments)
                {
                    switch (segment.Type)
                    {
                        case PdfPathSegmentType.Point:
                            target.CurrentPosition = ((PdfPointSegment)segment).Value;
                            break;

                        case PdfPathSegmentType.Line:
                            PdfLineSegment line = (PdfLineSegment)segment;
                            target.CurrentPosition = line.Start;
                            target.AppendLineTo(line.End);
                            break;

                        case PdfPathSegmentType.Bezier:
                            PdfBezierSegment bezier = (PdfBezierSegment)segment;
                            target.CurrentPosition = bezier.Start;
                            target.AppendCurveTo(bezier.FirstControl, bezier.SecondControl, bezier.End);
                            break;

                        case PdfPathSegmentType.Rectangle:
                            target.AppendRectangle(((PdfRectangleSegment)segment).Bounds);
                            break;

                        case PdfPathSegmentType.CloseSubpath:
                            target.ClosePath();
                            break;
                    }
                }
            }
        }

        private static void drawPath(PdfCanvas target, PdfPath path)
        {
            switch (path.PaintMode)
            {
                case PdfDrawMode.Fill:
                    target.FillPath(path.FillMode.Value);
                    break;

                case PdfDrawMode.FillAndStroke:
                    target.FillAndStrokePath(path.FillMode.Value);
                    break;

                case PdfDrawMode.Stroke:
                    target.StrokePath();
                    break;

                default:
                    target.ResetPath();
                    break;
            }
        }

        private static void drawText(PdfCanvas target, PdfTextData td)
        {
            target.TextRenderingMode = td.RenderingMode;
            setBrush(target.Brush, td.Brush);
            setPen(target.Pen, td.Pen);

            target.TextPosition = PdfPoint.Empty;
            target.FontSize = td.FontSize;
            target.Font = td.Font;
            target.CharacterSpacing = td.CharacterSpacing;
            target.WordSpacing = td.WordSpacing;
            target.TextHorizontalScaling = td.HorizontalScaling;

            target.TranslateTransform(td.Position.X, td.Position.Y);
            target.Transform(td.TransformationMatrix);

            target.DrawString(td.GetCharacterCodes());
        }
    }
}