Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ExtractPaths
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Const PathToFile As String = "ExtractPaths.pdf"

            Using pdf = New PdfDocument("..\Sample Data\gmail-cheat-sheet.pdf")
                Using copy As PdfDocument = pdf.CopyPages(0, 1)
                    Dim sourcePage As PdfPage = copy.Pages(0)
                    Dim copyPage As PdfPage = copy.AddPage()

                    copyPage.Group = sourcePage.Group
                    copyPage.UserUnit = sourcePage.UserUnit
                    copyPage.Rotation = sourcePage.Rotation
                    copyPage.MediaBox = sourcePage.MediaBox
                    If sourcePage.CropBox <> sourcePage.MediaBox Then
                        copyPage.CropBox = sourcePage.CropBox
                    End If

                    Dim options = New PdfObjectExtractionOptions With {
                        .FlattenMarkedContent = True,
                        .FlattenXObjects = True
                    }
                    Dim target As PdfCanvas = copyPage.Canvas
                    For Each obj As PdfPageObject In sourcePage.GetObjects(options)
                        If obj.Type <> PdfPageObjectType.Path Then
                            Continue For
                        End If

                        target.SaveState()
                        If True Then
                            Dim path As PdfPath = DirectCast(obj, PdfPath)
                            target.Transform(path.TransformationMatrix)
                            SetClipRegion(target, path.ClipRegion)
                            SetBrushAndPen(target, path)

                            AppendPath(target, path)
                            DrawPath(target, path)
                        End If
                        target.RestoreState()
                    Next

                    copy.RemovePage(0)

                    copy.Save(PathToFile)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Sub SetClipRegion(canvas As PdfCanvas, clipRegion As PdfClipRegion)
            If clipRegion.IntersectedPaths.Count = 0 Then
                Return
            End If

            Dim transformationBefore As PdfMatrix = canvas.TransformationMatrix
            Try
                For Each clipPath As PdfPath In clipRegion.IntersectedPaths
                    canvas.ResetTransform()
                    canvas.Transform(clipPath.TransformationMatrix)
                    AppendPath(canvas, clipPath)
                    canvas.SetClip(clipPath.ClipMode.Value)
                Next
            Finally
                canvas.ResetTransform()
                canvas.Transform(transformationBefore)
            End Try
        End Sub

        Private Shared Sub SetBrushAndPen(target As PdfCanvas, path As PdfPath)
            If path.PaintMode = PdfDrawMode.Fill OrElse path.PaintMode = PdfDrawMode.FillAndStroke Then
                target.Brush.Opacity = path.Brush.Opacity

                Dim color As PdfColor = path.Brush.Color
                If color IsNot Nothing Then
                    target.Brush.Color = path.Brush.Color
                End If
            End If

            If path.PaintMode = PdfDrawMode.Stroke OrElse path.PaintMode = PdfDrawMode.FillAndStroke Then
                target.Pen.Opacity = path.Pen.Opacity
                target.Pen.Width = path.Pen.Width
                target.Pen.DashPattern = path.Pen.DashPattern
                target.Pen.EndCap = path.Pen.EndCap
                target.Pen.LineJoin = path.Pen.LineJoin
                target.Pen.MiterLimit = path.Pen.MiterLimit

                Dim color As PdfColor = path.Pen.Color
                If color IsNot Nothing Then
                    target.Pen.Color = path.Pen.Color
                End If
            End If
        End Sub

        Private Shared Sub AppendPath(target As PdfCanvas, path As PdfPath)
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

        Private Shared Sub DrawPath(target As PdfCanvas, path As PdfPath)
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
    End Class
End Namespace