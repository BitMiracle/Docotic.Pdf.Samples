using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractPaths
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            const string PathToFile = "ExtractPaths.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\gmail-cheat-sheet.pdf"))
            {
                using PdfDocument copy = pdf.CopyPages(0, 1);
                PdfPage sourcePage = copy.Pages[0];
                PdfPage copyPage = copy.AddPage();

                copyPage.Group = sourcePage.Group;
                copyPage.UserUnit = sourcePage.UserUnit;
                copyPage.Rotation = sourcePage.Rotation;
                copyPage.MediaBox = sourcePage.MediaBox;
                if (sourcePage.CropBox != sourcePage.MediaBox)
                    copyPage.CropBox = sourcePage.CropBox;

                var options = new PdfObjectExtractionOptions
                {
                    FlattenMarkedContent = true,
                    FlattenXObjects = true,
                };
                PdfCanvas target = copyPage.Canvas;
                foreach (PdfPageObject obj in sourcePage.GetObjects(options))
                {
                    if (obj.Type != PdfPageObjectType.Path)
                        continue;

                    target.SaveState();
                    {
                        PdfPath path = (PdfPath)obj;
                        target.Transform(path.TransformationMatrix);
                        SetClipRegion(target, path.ClipRegion);
                        SetBrushAndPen(target, path);

                        AppendPath(target, path);
                        DrawPath(target, path);
                    }
                    target.RestoreState();
                }

                copy.RemovePage(0);

                copy.Save(PathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }

        private static void SetClipRegion(PdfCanvas canvas, PdfClipRegion clipRegion)
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
                    AppendPath(canvas, clipPath);
                    canvas.SetClip(clipPath.ClipMode!.Value);
                }
            }
            finally
            {
                canvas.ResetTransform();
                canvas.Transform(transformationBefore);
            }
        }

        private static void SetBrushAndPen(PdfCanvas target, PdfPath path)
        {
            if (path.PaintMode == PdfDrawMode.Fill || path.PaintMode == PdfDrawMode.FillAndStroke)
            {
                target.Brush.Opacity = path.Brush.Opacity;

                PdfColor? color = path.Brush.Color;
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

                PdfColor? color = path.Pen.Color;
                if (color != null)
                    target.Pen.Color = path.Pen.Color;
            }
        }

        private static void AppendPath(PdfCanvas target, PdfPath path)
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

        private static void DrawPath(PdfCanvas target, PdfPath path)
        {
            switch (path.PaintMode)
            {
                case PdfDrawMode.Fill:
                    target.FillPath(path.FillMode!.Value);
                    break;

                case PdfDrawMode.FillAndStroke:
                    target.FillAndStrokePath(path.FillMode!.Value);
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
