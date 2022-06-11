Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports BitMiracle.Docotic.Pdf
Imports Tesseract

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class OcrGarbledText
        Public Shared Sub Main()
            ' NOTE 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.
            Dim documentText = New StringBuilder()

            Using pdf = New PdfDocument("..\Sample data\Broken text encoding.pdf")
                Dim location As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                Dim tessData As String = Path.Combine(location, "tessdata")

                Using engine = New TesseractEngine(tessData, "eng", EngineMode.LstmOnly)
                    For i As Integer = 0 To pdf.PageCount - 1

                        If documentText.Length > 0 Then documentText.Append(vbCrLf & vbCrLf)

                        ' get list of character codes that are mapped to incorrect Unicode values
                        Dim unmappedCharacterCodes = New List(Of PdfCharacterCode)()
                        Dim firstPassOptions = New PdfTextExtractionOptions With {
                            .UnmappedCharacterCodeHandler = Function(c)
                                                                unmappedCharacterCodes.Add(c)
                                                                Return Nothing
                                                            End Function
                        }
                        Dim page As PdfPage = pdf.Pages(i)
                        Dim text As String = page.GetText(firstPassOptions)
                        If unmappedCharacterCodes.Count = 0 Then
                            documentText.Append(text)
                            Continue For
                        End If

                        ' perform OCR to get correct Unicode values
                        Dim recognizedText As String() = ocrCharacterCodes(unmappedCharacterCodes, engine)

                        ' extract text and fix Unicode values for unmapped character codes
                        Dim index As Integer = 0
                        Dim options = New PdfTextExtractionOptions With {
                            .UnmappedCharacterCodeHandler = Function(c)
                                                                Dim t As String = recognizedText(index)
                                                                index += 1
                                                                Return If(t, " ")
                                                            End Function
                        }
                        documentText.Append(page.GetText(options))
                    Next
                End Using
            End Using

            Using writer = New StreamWriter("OcrGarbledText.txt")
                writer.Write(documentText.ToString())
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub

        Private Shared Function ocrCharacterCodes(ByVal charCodes As List(Of PdfCharacterCode), ByVal engine As TesseractEngine) As String()
            Dim recognizedText As String() = New String(charCodes.Count - 1) {}

            Dim rasterizer = New PdfTextRasterizer With {
                .HorizontalResolution = 300,
                .VerticalResolution = 300,
                .Height = 20,
                .CharacterSpacing = 1.5 ' slightly increase distance between characters to improve OCR quality
            }
            Using charCodeImage = New MemoryStream()
                Dim widthsPoints As Double() = rasterizer.Save(charCodeImage, charCodes)
                Dim positionsPoints As Double() = New Double(widthsPoints.Length - 1) {}

                For j As Integer = 1 To positionsPoints.Length - 1
                    positionsPoints(j) = positionsPoints(j - 1) + widthsPoints(j - 1) + rasterizer.CharacterSpacing
                Next

                Using img As Pix = Pix.LoadFromMemory(charCodeImage.ToArray())

                    Using recognizedPage As Page = engine.Process(img)

                        Using iter As ResultIterator = recognizedPage.GetIterator()
                            Dim lastCharCodeIndex As Integer = -1
                            Const Level As PageIteratorLevel = PageIteratorLevel.Symbol
                            iter.Begin()

                            Do
                                Dim bounds As Rect = Nothing
                                If iter.TryGetBoundingBox(Level, bounds) Then
                                    Dim bestIntersectionWidth As Double = 0
                                    Dim bestMatchIndex As Integer = -1

                                    For c As Integer = lastCharCodeIndex + 1 To charCodes.Count - 1
                                        Dim x As Double = pointsToPixels(positionsPoints(c), rasterizer.HorizontalResolution)
                                        Dim width As Double = pointsToPixels(widthsPoints(c), rasterizer.HorizontalResolution)

                                        If bounds.X2 < x Then Exit For

                                        If x + width < bounds.X1 Then Continue For

                                        Dim intersection As Double = Math.Min(bounds.X2, x + width) - Math.Max(bounds.X1, x)
                                        If bestIntersectionWidth < intersection Then
                                            bestIntersectionWidth = intersection
                                            bestMatchIndex = c
                                        End If
                                    Next

                                    If bestMatchIndex >= 0 Then
                                        recognizedText(bestMatchIndex) = iter.GetText(Level)
                                        lastCharCodeIndex = bestMatchIndex
                                    End If
                                End If
                            Loop While iter.[Next](Level)
                        End Using
                    End Using
                End Using
            End Using

            Return recognizedText
        End Function

        Private Shared Function pointsToPixels(ByVal points As Double, ByVal dpi As Double) As Double
            Return points * dpi / 72
        End Function
    End Class
End Namespace