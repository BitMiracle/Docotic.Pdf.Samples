Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Class PageObjectCopier
        Private ReadOnly m_document As PdfDocument
        Private ReadOnly m_options As PdfObjectExtractionOptions
        Private ReadOnly m_xobjectCopies As New Dictionary(Of String, PdfXObject)()

        Public Sub New(document As PdfDocument, Optional options As PdfObjectExtractionOptions = Nothing)
            m_document = document
            m_options = If(options, New PdfObjectExtractionOptions())
        End Sub

        Public Sub Copy(sourcePage As PdfPage, copyPage As PdfPage)
            copyPage.Group = sourcePage.Group
            copyPage.UserUnit = sourcePage.UserUnit
            copyPage.Rotation = sourcePage.Rotation
            copyPage.MediaBox = sourcePage.MediaBox
            If sourcePage.CropBox <> sourcePage.MediaBox Then
                copyPage.CropBox = sourcePage.CropBox
            End If

            Dim objects As IEnumerable(Of PdfPageObject) = sourcePage.GetObjects(m_options)
            copyPageObjects(objects, copyPage.Canvas)
        End Sub

        Public Sub copyPageObjects(objects As IEnumerable(Of PdfPageObject), target As PdfCanvas)
            For Each obj As PdfPageObject In objects
                If (obj.Type = PdfPageObjectType.MarkedContent) Then
                    Dim markedContent As PdfMarkedContent = DirectCast(obj, PdfMarkedContent)

                    target.BeginMarkedContent(markedContent.Tag.Name, markedContent.Properties)
                    copyPageObjects(markedContent.GetObjects(), target)
                    target.EndMarkedContent()

                    Continue For
                End If

                target.SaveState()
                setClipRegion(target, obj.ClipRegion)

                If obj.Type = PdfPageObjectType.Path Then
                    Dim path As PdfPath = DirectCast(obj, PdfPath)
                    target.Transform(path.TransformationMatrix)
                    If path.PaintMode = PdfDrawMode.Fill OrElse path.PaintMode = PdfDrawMode.FillAndStroke Then setBrush(target.Brush, path.Brush)
                    If path.PaintMode = PdfDrawMode.Stroke OrElse path.PaintMode = PdfDrawMode.FillAndStroke Then setPen(target.Pen, path.Pen)
                    target.BlendMode = path.BlendMode

                    appendPath(target, path)
                    drawPath(target, path)
                ElseIf obj.Type = PdfPageObjectType.Image Then
                    Dim image As PdfPaintedImage = DirectCast(obj, PdfPaintedImage)
                    target.TranslateTransform(image.Position.X, image.Position.Y)
                    target.Transform(image.TransformationMatrix)

                    setBrush(target.Brush, image.Brush)
                    target.BlendMode = image.BlendMode

                    target.DrawImage(image.Image, 0, 0, 0)
                ElseIf obj.Type = PdfPageObjectType.Text Then
                    Dim text As PdfTextData = CType(obj, PdfTextData)
                    drawText(target, text)
                ElseIf obj.Type = PdfPageObjectType.XObject Then
                    Dim xobj As PdfPaintedXObject = CType(obj, PdfPaintedXObject)
                    setBrush(target.Brush, xobj.Brush)
                    setPen(target.Pen, xobj.Pen)
                    target.BlendMode = xobj.BlendMode
                    target.Transform(xobj.TransformationMatrix)

                    ' Extract and copy page objects from XObject
                    Dim copyXObject As PdfXObject = Nothing
                    Dim srcXObject As PdfXObject = xobj.XObject
                    If Not m_xobjectCopies.TryGetValue(srcXObject.Id, copyXObject) Then
                        copyXObject = m_document.CreateXObject()
                        m_xobjectCopies.Add(srcXObject.Id, copyXObject)

                        copyXObject.BoundingBox = srcXObject.BoundingBox
                        copyXObject.Matrix = srcXObject.Matrix
                        copyXObject.Group = srcXObject.Group

                        Dim nestedObjects As IEnumerable(Of PdfPageObject) = srcXObject.GetObjects(m_options)
                        copyPageObjects(nestedObjects, copyXObject.Canvas)
                    End If

                    target.DrawXObject(copyXObject, PdfPoint.Empty)
                End If

                target.RestoreState()
            Next
        End Sub


        Private Shared Sub setBrush(dst As PdfBrush, src As PdfBrushInfo)
            Dim color As PdfColor = src.Color
            If color IsNot Nothing Then dst.Color = color

            dst.Opacity = src.Opacity

            Dim pattern = src.Pattern
            If pattern IsNot Nothing Then dst.Pattern = pattern
        End Sub

        Private Shared Sub setPen(dst As PdfPen, src As PdfPenInfo)
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

        Private Shared Sub drawText(target As PdfCanvas, td As PdfTextData)
            target.TextRenderingMode = td.RenderingMode
            setBrush(target.Brush, td.Brush)
            setPen(target.Pen, td.Pen)
            target.BlendMode = td.BlendMode

            target.FontSize = td.FontSize
            target.Font = td.Font
            target.TextPosition = PdfPoint.Empty
            target.CharacterSpacing = td.CharacterSpacing
            target.WordSpacing = td.WordSpacing
            target.TextHorizontalScaling = td.HorizontalScaling

            target.TranslateTransform(td.Position.X, td.Position.Y)
            target.Transform(td.TransformationMatrix)

            target.DrawString(td.GetCharCodes())
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
    End Class
End Namespace