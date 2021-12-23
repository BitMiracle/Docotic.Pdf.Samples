using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractPaths
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            const string PathToFile = "ExtractPaths.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\gmail-cheat-sheet.pdf"))
            {
                using (PdfDocument copy = pdf.CopyPages(0, 1))
                {
                    PdfPage sourcePage = copy.Pages[0];
                    PdfPage copyPage = copy.AddPage();

                    copyPage.Rotation = sourcePage.Rotation;
                    copyPage.MediaBox = sourcePage.MediaBox;
                    if (sourcePage.CropBox != sourcePage.MediaBox)
                        copyPage.CropBox = sourcePage.CropBox;

                    PdfCanvas target = copyPage.Canvas;
                    foreach (PdfPageObject obj in sourcePage.GetObjects())
                    {
                        if (obj.Type != PdfPageObjectType.Path)
                            continue;

                        target.SaveState();
                        {
                            PdfPath path = (PdfPath)obj;
                            target.Transform(path.TransformationMatrix);
                            setClipRegion(target, path.ClipRegion);
                            setBrushAndPen(target, path);

                            appendPath(target, path);
                            drawPath(target, path);
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

        private static void setBrushAndPen(PdfCanvas target, PdfPath path)
        {
            if (path.PaintMode == PdfDrawMode.Fill || path.PaintMode == PdfDrawMode.FillAndStroke)
            {
                target.Brush.Opacity = path.Brush.Opacity;

                PdfColor color = path.Brush.Color;
                if (color != null)
                    target.Brush.Color = path.Brush.Color;
            }

            if (path.PaintMode == PdfDrawMode.Stroke || path.PaintMode == PdfDrawMode.FillAndStroke)
            {
                target.Pen.Opacity = path.Pen.Opacity;
                target.Pen.Width = path.Pen.Width;
                target.Pen.DashPattern = path.Pen.DashPattern;
                target.Pen.EndCap = path.Pen.EndCap;
                target.Pen.LineJoin = path.Pen.LineJoin;
                target.Pen.MiterLimit = path.Pen.MiterLimit;

                PdfColor color = path.Pen.Color;
                if (color != null)
                    target.Pen.Color = path.Pen.Color;
            }
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
    }
}
