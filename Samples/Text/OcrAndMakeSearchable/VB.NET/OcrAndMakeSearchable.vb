Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports Tesseract

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class OcrAndMakeSearchable
        Public Shared Sub Main()
            ' NOTE 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf = New PdfDocument("Sample data/Freedman Scora.pdf")
                ' This font Is used to draw all recognized text chunks in PDF.
                ' Make sure that the font defines all glyphs for the target language.
                Dim universalFont As PdfFont = pdf.AddFont("Arial")

                Using engine = New TesseractEngine("tessdata", "eng", EngineMode.LstmOnly)
                    For i As Integer = 0 To pdf.PageCount - 1
                        Dim page As PdfPage = pdf.Pages(i)

                        ' Simple check if the page contains searchable text.
                        ' We do Not need to do OCR in that case.
                        If Not String.IsNullOrEmpty(page.GetText().Trim()) Then Continue For

                        Dim canvas = page.Canvas
                        canvas.Font = universalFont

                        ' Produce invisible, but searchable text
                        canvas.TextRenderingMode = PdfTextRenderingMode.NeitherFillNorStroke

                        Const Dpi As Integer = 200
                        Const ImageToPdfScaleFactor As Double = 72.0 / Dpi

                        For Each word As RecognizedTextChunk In recognizeWords(page, engine, Dpi, $"page_{i}.png")
                            If word.Confidence < 80 Then Console.WriteLine($"Possible recognition error: low confidence {word.Confidence} for word '{word.Text}'")

                            Dim bounds As Rect = word.Bounds
                            Dim pdfBounds As PdfRectangle = New PdfRectangle(bounds.X1 * ImageToPdfScaleFactor, bounds.Y1 * ImageToPdfScaleFactor, bounds.Width * ImageToPdfScaleFactor, bounds.Height * ImageToPdfScaleFactor)

                            tuneFontSize(canvas, pdfBounds.Width, word.Text)

                            Dim distanceToBaseLine As Double = getDistanceToBaseline(canvas.Font, canvas.FontSize)
                            canvas.DrawString(pdfBounds.Left, pdfBounds.Bottom - distanceToBaseLine, word.Text)
                        Next
                    Next
                End Using

                universalFont.RemoveUnusedGlyphs()
                Const Result As String = "OcrAndMakeSearchable.pdf"
                pdf.Save(Result)
                Process.Start(Result)
            End Using
        End Sub

        Private Shared Iterator Function recognizeWords(ByVal page As PdfPage, ByVal engine As TesseractEngine, ByVal resolution As Integer, ByVal tempFileName As String) As IEnumerable(Of RecognizedTextChunk)
            ' Save PDF page as high-resolution image
            Dim options As PdfDrawOptions = PdfDrawOptions.Create()
            options.BackgroundColor = New PdfRgbColor(255, 255, 255)
            options.HorizontalResolution = resolution
            options.VerticalResolution = resolution
            page.Save(tempFileName, options)

            Using img = Pix.LoadFromFile(tempFileName)
                Using recognizedPage = engine.Process(img)
                    Using iter As ResultIterator = recognizedPage.GetIterator()
                        Const Level As PageIteratorLevel = PageIteratorLevel.Word
                        iter.Begin()
                        Do
                            Dim bounds As Rect = Nothing
                            If iter.TryGetBoundingBox(Level, bounds) Then
                                Dim text As String = iter.GetText(Level)
                                Dim confidence As Single = iter.GetConfidence(Level)
                                Yield New RecognizedTextChunk With
                                {
                                    .Text = text,
                                    .Bounds = bounds,
                                    .Confidence = confidence
                                }
                            End If
                        Loop While iter.[Next](Level)
                    End Using
                End Using
            End Using
        End Function

        Private Shared Sub tuneFontSize(ByVal canvas As PdfCanvas, ByVal targetTextWidth As Double, ByVal text As String)
            Const [Step] As Double = 0.1

            Dim bestFontSize As Double = canvas.FontSize
            Dim bestDiff As Double = targetTextWidth - canvas.GetTextWidth(text)
            While True
                Dim diffSign As Integer = Math.Sign(bestDiff)
                If diffSign = 0 Then Exit While

                ' try neigbour font size
                canvas.FontSize += diffSign * [Step]

                Dim newDiff As Double = targetTextWidth - canvas.GetTextWidth(text)
                If Math.Abs(newDiff) > Math.Abs(bestDiff) Then
                    canvas.FontSize = bestFontSize
                    Exit While
                End If

                bestDiff = newDiff
                bestFontSize = canvas.FontSize
            End While
        End Sub

        Private Shared Function getDistanceToBaseline(ByVal font As PdfFont, ByVal fontSize As Double) As Double
            Return font.TopSideBearing * font.TransformationMatrix.M22 * fontSize
        End Function

        Private Class RecognizedTextChunk
            Public Property Text As String
            Public Property Bounds As Rect
            Public Property Confidence As Single
        End Class
    End Class
End Namespace