Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.Versioning
Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf
Imports BitMiracle.Docotic.Pdf.Gdi

Namespace BitMiracle.Docotic.Pdf.Samples
    <SupportedOSPlatform("windows")>
    Public NotInheritable Class ExtractPageObjects
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Const PathToFile As String = "ExtractPageObjects.png"

            Using pdf = New PdfDocument("..\Sample Data\gmail-cheat-sheet.pdf")
                Dim page As PdfPage = pdf.Pages(0)

                Const TargetResolution As Single = 300
                Dim scaleFactor As Double = TargetResolution / page.Resolution
                Using bitmap As New Bitmap(CInt(page.Width * scaleFactor), CInt(page.Height * scaleFactor))
                    bitmap.SetResolution(TargetResolution, TargetResolution)

                    Using gr = Graphics.FromImage(bitmap)
                        gr.SmoothingMode = SmoothingMode.HighQuality
                        gr.PageUnit = GraphicsUnit.Point

                        Dim userUnit As Single = page.UserUnit
                        gr.ScaleTransform(userUnit, userUnit)

                        gr.SetClip(page.CropBox.ToRectangleF(page.Height), CombineMode.Intersect)

                        ' We do not need to handle marked content and XObjects in this sample.
                        ' You may want to set these options to false in order to:
                        ' * Respect transparency groups for XObjects
                        ' * Respect layers or tagged content
                        '
                        ' Look at the EditPageContent sample that shows how to handle marked content and XObjects.
                        Dim options = New PdfObjectExtractionOptions With {
                            .FlattenMarkedContent = True,
                            .FlattenXObjects = True
                        }
                        For Each obj As PdfPageObject In page.GetObjects(options)
                            Select Case obj.Type
                                Case PdfPageObjectType.Text
                                    DrawText(gr, DirectCast(obj, PdfTextData), userUnit)

                                Case PdfPageObjectType.Image
                                    DrawImage(gr, DirectCast(obj, PdfPaintedImage), userUnit)

                                Case PdfPageObjectType.Path
                                    DrawPath(gr, DirectCast(obj, PdfPath), userUnit)
                            End Select
                        Next
                    End Using

                    bitmap.Save(PathToFile, ImageFormat.Png)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Sub DrawText(gr As Graphics, td As PdfTextData, userUnit As Single)
            If td.RenderingMode = PdfTextRenderingMode.NeitherFillNorStroke OrElse td.RenderingMode = PdfTextRenderingMode.AddToPath Then
                Return
            End If

            Dim fontSizeAbs As Double = Math.Abs(td.FontSize)
            If fontSizeAbs < 0.001 Then
                Return
            End If

            If Math.Abs(td.Bounds.Width) < 0.001 OrElse Math.Abs(td.Bounds.Height) < 0.001 Then
                Return
            End If

            SaveStateAndDraw(gr, td.ClipRegion, userUnit,
                Sub()
                    Using font As Font = ToGdiFont(td.Font, fontSizeAbs)
                        Using brush As Brush = ToGdiBrush(td.Brush)
                            gr.TranslateTransform(td.Position.X, td.Position.Y)
                            ConcatMatrix(gr, td.TransformationMatrix)
                            If (Math.Sign(td.FontSize) < 0) Then
                                gr.ScaleTransform(1, -1)
                            End If

                            gr.DrawString(td.GetText(), font, brush, PointF.Empty)
                        End Using
                    End Using
                End Sub
            )
        End Sub

        Private Shared Sub DrawImage(gr As Graphics, im As PdfPaintedImage, userUnit As Single)
            ' Do not render images with zero width or height. GDI+ throws OutOfMemoryException
            ' in x86 processes for regular image size (e.g. 1x51) and very small transformation
            ' matrix (e.g. { 0.000005, 0, 0, 0.003, x, y }).
            Dim paintedImageSize As PdfSize = im.Bounds.Size
            If Math.Abs(paintedImageSize.Width) < 0.001 OrElse Math.Abs(paintedImageSize.Height) < 0.001 Then
                Return
            End If

            ' ignore mask images in this sample
            If im.Image.IsMask Then
                Return
            End If

            Using stream = New MemoryStream()
                im.Image.Save(stream)
                Using bitmap As Bitmap = DirectCast(Image.FromStream(stream), Bitmap)
                    SaveStateAndDraw(gr, im.ClipRegion, userUnit,
                        Sub()
                            gr.TranslateTransform(im.Position.X, im.Position.Y)
                            ConcatMatrix(gr, im.TransformationMatrix)

                            ' Important for rendering of neighbour image tiles
                            gr.PixelOffsetMode = PixelOffsetMode.Half

                            Dim imageSize As New PdfSize(im.Image.Width, im.Image.Height)
                            If imageSize.Width < bitmap.Width AndAlso imageSize.Height < bitmap.Height Then
                                ' the bitmap produced from the image is larger than the image.
                                ' usually this happens when an image has a mask image which is larger than the image itself.
                                Dim current As InterpolationMode = gr.InterpolationMode
                                gr.InterpolationMode = InterpolationMode.HighQualityBicubic
                                gr.DrawImage(bitmap, 0, 0, CSng(imageSize.Width), CSng(imageSize.Height))
                                gr.InterpolationMode = current
                            Else
                                ' bitmap has the same size,
                                ' or one of its dimensions is longer than the corresponding image dimension
                                gr.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height)
                            End If

                        End Sub
                    )
                End Using
            End Using
        End Sub

        Private Shared Sub DrawPath(gr As Graphics, path As PdfPath, userUnit As Single)
            If Not path.PaintMode.HasValue Then
                Return
            End If

            SaveStateAndDraw(gr, path.ClipRegion, userUnit,
                Sub()
                    ConcatMatrix(gr, path.TransformationMatrix)

                    Using gdiPath = New GraphicsPath()
                        ToGdiPath(path, gdiPath)

                        FillStrokePath(gr, path, gdiPath)
                    End Using
                End Sub
            )
        End Sub

        Private Shared Sub SaveStateAndDraw(gr As Graphics, clipRegion As PdfClipRegion, userUnit As Single, draw As Action)
            Dim state = gr.Save()
            Try
                SetClipRegion(gr, clipRegion, userUnit)

                draw()
            Finally
                gr.Restore(state)
            End Try
        End Sub

        Private Shared Sub SetClipRegion(gr As Graphics, clipRegion As PdfClipRegion, userUnit As Single)
            If clipRegion.IntersectedPaths.Count = 0 Then
                Return
            End If

            Dim transformationBefore As Matrix = gr.Transform
            Try
                gr.Transform = New Matrix(userUnit, 0, 0, userUnit, 0, 0)
                For Each clipPath As PdfPath In clipRegion.IntersectedPaths
                    Using gdiPath = New GraphicsPath()
                        ToGdiPath(clipPath, gdiPath)
                        gdiPath.Transform(ToGdiMatrix(clipPath.TransformationMatrix))

                        gdiPath.FillMode = DirectCast(clipPath.ClipMode.Value, FillMode)
                        gr.SetClip(gdiPath, CombineMode.Intersect)
                    End Using
                Next
            Finally
                gr.Transform = transformationBefore
            End Try
        End Sub

        Private Shared Sub ToGdiPath(path As PdfPath, gdiPath As GraphicsPath)
            For Each subpath As PdfSubpath In path.Subpaths
                gdiPath.StartFigure()

                For Each segment As PdfPathSegment In subpath.Segments
                    Select Case segment.Type
                        Case PdfPathSegmentType.Point
                            ' A singlepoint open subpath produces no output.

                        Case PdfPathSegmentType.Line
                            Dim line As PdfLineSegment = DirectCast(segment, PdfLineSegment)
                            gdiPath.AddLine(line.Start.ToPointF(), line.[End].ToPointF())

                        Case PdfPathSegmentType.Bezier
                            Dim bezier As PdfBezierSegment = DirectCast(segment, PdfBezierSegment)
                            gdiPath.AddBezier(bezier.Start.ToPointF(), bezier.FirstControl.ToPointF(), bezier.SecondControl.ToPointF(), bezier.[End].ToPointF())

                        Case PdfPathSegmentType.Rectangle
                            Dim rect As RectangleF = DirectCast(segment, PdfRectangleSegment).Bounds.ToRectangleF()

                            ' GDI+ does not render rectangles with a negative or
                            ' very small width and height. Render such rectangles by lines,
                            ' but respect direction. Otherwise, non-zero winding rule for
                            ' path filling will not work.
                            gdiPath.AddLines({rect.Location,
                                             New PointF(rect.X + rect.Width, rect.Y),
                                             New PointF(rect.X + rect.Width, rect.Y + rect.Height),
                                             New PointF(rect.X, rect.Y + rect.Height), rect.Location})

                        Case PdfPathSegmentType.CloseSubpath
                            gdiPath.CloseFigure()
                    End Select
                Next
            Next
        End Sub

        Private Shared Sub FillStrokePath(gr As Graphics, path As PdfPath, gdiPath As GraphicsPath)
            Dim paintMode As PdfDrawMode = path.PaintMode.Value
            If paintMode = PdfDrawMode.Fill OrElse paintMode = PdfDrawMode.FillAndStroke Then
                Using brush = ToGdiBrush(path.Brush)
                    gdiPath.FillMode = DirectCast(path.FillMode.Value, FillMode)
                    gr.FillPath(brush, gdiPath)
                End Using
            End If

            If paintMode = PdfDrawMode.Stroke OrElse paintMode = PdfDrawMode.FillAndStroke Then
                Using pen = ToGdiPen(path.Pen)
                    gr.DrawPath(pen, gdiPath)
                End Using
            End If
        End Sub

        Private Shared Sub ConcatMatrix(gr As Graphics, transformation As PdfMatrix)
            Using m = ToGdiMatrix(transformation)
                Using current As Matrix = gr.Transform
                    current.Multiply(m, MatrixOrder.Prepend)
                    gr.Transform = current
                End Using
            End Using
        End Sub

        Private Shared Function ToGdiMatrix(matrix As PdfMatrix) As Matrix
            Return New Matrix(matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.OffsetX, matrix.OffsetY)
        End Function

        Private Shared Function ToGdiBrush(brush As PdfBrushInfo) As Brush
            Return New SolidBrush(ToGdiColor(brush.Color, brush.Opacity))
        End Function

        Private Shared Function ToGdiPen(pen As PdfPenInfo) As Pen
            Return New Pen(ToGdiColor(pen.Color, pen.Opacity), pen.Width) With {
                .LineJoin = ToGdiLineJoin(pen.LineJoin),
                .EndCap = ToGdiLineCap(pen.EndCap),
                .MiterLimit = CSng(pen.MiterLimit)
            }
        End Function

        Private Shared Function ToGdiColor(pdfColor As PdfColor, opacityPercent As Integer) As Color
            If pdfColor Is Nothing Then
                Return Color.Empty
            End If

            Dim rgbColor As PdfRgbColor = pdfColor.ToRgb()

            Dim alpha As Integer = 255
            If opacityPercent < 100 AndAlso opacityPercent >= 0 Then
                alpha = CInt(255.0 * opacityPercent / 100.0)
            End If

            Return Color.FromArgb(alpha, rgbColor.R, rgbColor.G, rgbColor.B)
        End Function

        Private Shared Function ToGdiFont(font As PdfFont, fontSize As Double) As Font
            Dim fontName As String = GetFontName(font)
            Dim fontStyle As FontStyle = GetFontStyle(font)

            Return New Font(fontName, CSng(fontSize), fontStyle)
        End Function

        Private Shared Function ToGdiLineCap(pdfLineCap As PdfLineCap) As LineCap
            Select Case pdfLineCap
                Case PdfLineCap.ButtEnd
                    Return LineCap.Flat

                Case PdfLineCap.Round
                    Return LineCap.Round

                Case PdfLineCap.ProjectingSquare
                    Return LineCap.Square

                Case Else
                    Throw New InvalidOperationException("We should never be here")
            End Select
        End Function

        Private Shared Function ToGdiLineJoin(pdfLineJoin As PdfLineJoin) As LineJoin
            Select Case pdfLineJoin
                Case PdfLineJoin.Miter
                    Return LineJoin.Miter

                Case PdfLineJoin.Round
                    Return LineJoin.Round

                Case PdfLineJoin.Bevel
                    Return LineJoin.Bevel

                Case Else
                    Throw New InvalidOperationException("We should never be here")
            End Select
        End Function

        Private Shared Function GetFontName(font As PdfFont) As String
            ' A trick to load a similar font from the system. Ideally, we should
            ' load font from raw bytes. Use PdfFont.Save methods to produce the bytes.
            Dim fontName As String = font.Name
            If (fontName.Contains("Courier")) Then
                Return "Courier New"
            End If

            If fontName.Contains("Times") Then
                Return "Times New Roman"
            End If

            If fontName.Contains("Garamond") Then
                Return "Garamond"
            End If

            If fontName.Contains("Arial") AndAlso Not fontName.Contains("Unicode") Then
                Return "Arial"
            End If

            Return font.Name
        End Function

        Private Shared Function GetFontStyle(font As PdfFont) As FontStyle
            Dim gdiFontStyle As FontStyle = FontStyle.Regular

            If font.Bold Then
                gdiFontStyle = gdiFontStyle Or FontStyle.Bold
            End If

            If font.Italic Then
                gdiFontStyle = gdiFontStyle Or FontStyle.Italic
            End If

            Return gdiFontStyle
        End Function
    End Class
End Namespace