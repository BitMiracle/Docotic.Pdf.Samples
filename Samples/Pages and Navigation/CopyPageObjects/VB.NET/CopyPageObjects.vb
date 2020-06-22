Imports System
Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CopyPageObjects
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const PathToFile As String = "CopyPageObjects.pdf"

            Using pdf = New PdfDocument("Sample Data/BRAILLE CODES WITH TRANSLATION.pdf")
                Using copy As PdfDocument = pdf.CopyPages(0, 1)
                    Dim sourcePage As PdfPage = copy.Pages(0)
                    Dim copyPage As PdfPage = copy.AddPage()

                    copyPage.Rotation = sourcePage.Rotation
                    copyPage.MediaBox = sourcePage.MediaBox
                    If sourcePage.CropBox <> sourcePage.MediaBox Then
                        copyPage.CropBox = sourcePage.CropBox
                    End If

                    Dim target As PdfCanvas = copyPage.Canvas
                    For Each obj As PdfPageObject In sourcePage.GetObjects()
                        target.SaveState()
                        setClipRegion(target, obj.ClipRegion)

                        If obj.Type = PdfPageObjectType.Path Then
                            Dim path As PdfPath = DirectCast(obj, PdfPath)
                            target.Transform(path.TransformationMatrix)
                            If path.PaintMode = PdfDrawMode.Fill OrElse path.PaintMode = PdfDrawMode.FillAndStroke Then setBrush(target.Brush, path.Brush)
                            If path.PaintMode = PdfDrawMode.Stroke OrElse path.PaintMode = PdfDrawMode.FillAndStroke Then setPen(target.Pen, path.Pen)

                            appendPath(target, path)
                            drawPath(target, path)
                        ElseIf obj.Type = PdfPageObjectType.Image Then
                            Dim image As PdfPaintedImage = DirectCast(obj, PdfPaintedImage)
                            target.TranslateTransform(image.Position.X, image.Position.Y)
                            target.Transform(image.TransformationMatrix)

                            setBrush(target.Brush, image.Brush)
                            target.DrawImage(image.Image, 0, 0, 0)
                        ElseIf obj.Type = PdfPageObjectType.Text Then
                            Dim text As PdfTextData = DirectCast(obj, PdfTextData)
                            drawText(target, text, copy)
                        End If

                        target.RestoreState()
                    Next

                    copy.RemovePage(0)

                    copy.Save(PathToFile)
                End Using
            End Using

            Process.Start(PathToFile)
        End Sub

        Private Shared Sub setClipRegion(canvas As PdfCanvas, clipRegion As PdfClipRegion)
            If clipRegion.IntersectedPaths.Count = 0 Then
                Return
            End If

            Dim transformationBefore As PdfMatrix = canvas.TransformationMatrix
            Try
                For Each clipPath As PdfPath In clipRegion.IntersectedPaths
                    canvas.ResetTransform()
                    canvas.Transform(clipPath.TransformationMatrix)
                    appendPath(canvas, clipPath)
                    canvas.SetClip(clipPath.ClipMode.Value)
                Next
            Finally
                canvas.ResetTransform()
                canvas.Transform(transformationBefore)
            End Try
        End Sub

        Private Shared Sub setBrush(ByVal dst As PdfBrush, ByVal src As PdfBrushInfo)
            Dim color As PdfColor = src.Color
            If color IsNot Nothing Then dst.Color = color

            dst.Opacity = src.Opacity

            Dim pattern = src.Pattern
            If pattern IsNot Nothing Then dst.Pattern = pattern
        End Sub

        Private Shared Sub setPen(ByVal dst As PdfPen, ByVal src As PdfPenInfo)
            Dim color As PdfColor = src.Color
            If color IsNot Nothing Then dst.Color = color

            Dim pattern = src.Pattern
            If pattern IsNot Nothing Then dst.Pattern = pattern

            dst.DashPattern = src.DashPattern
            dst.EndCap = src.EndCap
            dst.LineJoin = src.LineJoin
            dst.MiterLimit = src.MiterLimit
            dst.Opacity = src.Opacity
            dst.Width = src.Width
        End Sub

        Private Shared Sub appendPath(target As PdfCanvas, path As PdfPath)
            For Each subpath As PdfSubpath In path.Subpaths
                For Each segment As PdfPathSegment In subpath.Segments
                    Select Case segment.Type
                        Case PdfPathSegmentType.Point
                            target.CurrentPosition = DirectCast(segment, PdfPointSegment).Value
                            Exit Select

                        Case PdfPathSegmentType.Line
                            Dim line As PdfLineSegment = DirectCast(segment, PdfLineSegment)
                            target.CurrentPosition = line.Start
                            target.AppendLineTo(line.[End])
                            Exit Select

                        Case PdfPathSegmentType.Bezier
                            Dim bezier As PdfBezierSegment = DirectCast(segment, PdfBezierSegment)
                            target.CurrentPosition = bezier.Start
                            target.AppendCurveTo(bezier.FirstControl, bezier.SecondControl, bezier.[End])
                            Exit Select

                        Case PdfPathSegmentType.Rectangle
                            target.AppendRectangle(DirectCast(segment, PdfRectangleSegment).Bounds)
                            Exit Select

                        Case PdfPathSegmentType.CloseSubpath
                            target.ClosePath()
                            Exit Select
                    End Select
                Next
            Next
        End Sub

        Private Shared Sub drawPath(target As PdfCanvas, path As PdfPath)
            Select Case path.PaintMode
                Case PdfDrawMode.Fill
                    target.FillPath(path.FillMode.Value)
                    Exit Select

                Case PdfDrawMode.FillAndStroke
                    target.FillAndStrokePath(path.FillMode.Value)
                    Exit Select

                Case PdfDrawMode.Stroke
                    target.StrokePath()
                    Exit Select
                Case Else

                    target.ResetPath()
                    Exit Select
            End Select
        End Sub

        Private Shared Sub drawText(target As PdfCanvas, td As PdfTextData, pdf As PdfDocument)
            target.TextRenderingMode = td.RenderingMode
            setBrush(target.Brush, td.Brush)
            setPen(target.Pen, td.Pen)

            target.TextPosition = PdfPoint.Empty
            target.FontSize = td.FontSize
            target.Font = td.Font
            target.CharacterSpacing = td.CharacterSpacing
            target.WordSpacing = td.WordSpacing
            target.TextHorizontalScaling = td.HorizontalScaling

            target.TranslateTransform(td.Position.X, td.Position.Y)
            target.Transform(td.TransformationMatrix)

            target.DrawString(td.GetCharacterCodes())
        End Sub
    End Class
End Namespace