﻿using System.Collections.Generic;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class PageObjectCopier
    {
        private readonly PdfDocument m_document;
        private readonly PdfObjectExtractionOptions m_options;

        private readonly Dictionary<string, PdfXObject> m_xobjectCopies = new();

        public PageObjectCopier(PdfDocument document, PdfObjectExtractionOptions? options = null)
        {
            m_document = document;
            m_options = options ?? new PdfObjectExtractionOptions();
        }

        public void Copy(PdfPage sourcePage, PdfPage copyPage)
        {
            copyPage.Group = sourcePage.Group;
            copyPage.UserUnit = sourcePage.UserUnit;
            copyPage.Rotation = sourcePage.Rotation;
            copyPage.MediaBox = sourcePage.MediaBox;
            if (sourcePage.CropBox != sourcePage.MediaBox)
                copyPage.CropBox = sourcePage.CropBox;

            IEnumerable<PdfPageObject> objects = sourcePage.GetObjects(m_options);
            CopyPageObjects(objects, copyPage.Canvas);
        }

        public void CopyPageObjects(IEnumerable<PdfPageObject> objects, PdfCanvas target)
        {
            foreach (PdfPageObject obj in objects)
            {
                if (obj.Type == PdfPageObjectType.MarkedContent)
                {
                    PdfMarkedContent markedContent = (PdfMarkedContent)obj;

                    target.BeginMarkedContent(markedContent.Tag.Name, markedContent.Properties);
                    CopyPageObjects(markedContent.GetObjects(), target);
                    target.EndMarkedContent();

                    continue;
                }

                target.SaveState();
                SetClipRegion(target, obj.ClipRegion);

                if (obj.Type == PdfPageObjectType.Path)
                {
                    PdfPath path = (PdfPath)obj;
                    target.Transform(path.TransformationMatrix);

                    if (path.PaintMode == PdfDrawMode.Fill || path.PaintMode == PdfDrawMode.FillAndStroke)
                        SetBrush(target.Brush, path.Brush);

                    if (path.PaintMode == PdfDrawMode.Stroke || path.PaintMode == PdfDrawMode.FillAndStroke)
                        SetPen(target.Pen, path.Pen);

                    target.BlendMode = path.BlendMode;

                    AppendPath(target, path);
                    DrawPath(target, path);
                }
                else if (obj.Type == PdfPageObjectType.Image)
                {
                    PdfPaintedImage image = (PdfPaintedImage)obj;
                    target.TranslateTransform(image.Position.X, image.Position.Y);
                    target.Transform(image.TransformationMatrix);

                    SetBrush(target.Brush, image.Brush);
                    target.BlendMode = image.BlendMode;

                    target.DrawImage(image.Image, 0, 0, 0);
                }
                else if (obj.Type == PdfPageObjectType.Text)
                {
                    PdfTextData text = (PdfTextData)obj;
                    DrawText(target, text);
                }
                else if (obj.Type == PdfPageObjectType.XObject)
                {
                    PdfPaintedXObject xobj = (PdfPaintedXObject)obj;
                    SetBrush(target.Brush, xobj.Brush);
                    SetPen(target.Pen, xobj.Pen);
                    target.BlendMode = xobj.BlendMode;
                    target.Transform(xobj.TransformationMatrix);

                    // Extract and copy page objects from XObject
                    PdfXObject srcXObject = xobj.XObject;
                    if (!m_xobjectCopies.TryGetValue(srcXObject.Id, out PdfXObject? copyXObject))
                    {
                        copyXObject = m_document.CreateXObject();
                        m_xobjectCopies.Add(srcXObject.Id, copyXObject);

                        copyXObject.BoundingBox = srcXObject.BoundingBox;
                        copyXObject.Matrix = srcXObject.Matrix;
                        copyXObject.Group = srcXObject.Group;

                        IEnumerable<PdfPageObject> nestedObjects = srcXObject.GetObjects(m_options);
                        CopyPageObjects(nestedObjects, copyXObject.Canvas);
                    }

                    target.DrawXObject(copyXObject, PdfPoint.Empty);
                }

                target.RestoreState();
            }
        }

        private static void SetBrush(PdfBrush dst, PdfBrushInfo src)
        {
            PdfColor? color = src.Color;
            if (color != null)
                dst.Color = color;

            dst.Opacity = src.Opacity;

            var pattern = src.Pattern;
            if (pattern != null)
                dst.Pattern = pattern;
        }

        private static void SetPen(PdfPen dst, PdfPenInfo src)
        {
            PdfColor? color = src.Color;
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

        private static void DrawText(PdfCanvas target, PdfTextData td)
        {
            target.TextRenderingMode = td.RenderingMode;
            SetBrush(target.Brush, td.Brush);
            SetPen(target.Pen, td.Pen);
            target.BlendMode = td.BlendMode;

            target.FontSize = td.FontSize;
            target.Font = td.Font;
            target.TextPosition = PdfPoint.Empty;
            target.CharacterSpacing = td.CharacterSpacing;
            target.WordSpacing = td.WordSpacing;
            target.TextHorizontalScaling = td.HorizontalScaling;

            target.TranslateTransform(td.Position.X, td.Position.Y);
            target.Transform(td.TransformationMatrix);

            target.DrawString(td.GetCharCodes());
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
