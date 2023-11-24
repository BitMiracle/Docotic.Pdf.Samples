Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports BitMiracle.Docotic.Pdf
Imports Tesseract

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class FixGarbledText
        Public Shared Sub Main()
            ' NOTE 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.
            Dim documentText = New StringBuilder()

            Using pdf = New PdfDocument("..\Sample data\Broken text encoding.pdf")
                Dim location As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                If location Is Nothing Then
                    Console.WriteLine("Invalid assembly location")
                    Return
                End If

                Dim tessData As String = Path.Combine(location, "tessdata")

                Using engine = New TesseractEngine(tessData, "eng", EngineMode.LstmOnly)
                    For i As Integer = 0 To pdf.PageCount - 1

                        If documentText.Length > 0 Then documentText.Append(vbCrLf & vbCrLf)

                        ' Get a list of character codes that are not mapped to Unicode correctly
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
                            ' There are no unmapped characters. Use the extracted text as is
                            documentText.Append(text)
                            Continue For
                        End If

                        ' Perform OCR to get correct Unicode values
                        Dim recognizedText As String() = ocrCharacterCodes(unmappedCharacterCodes, engine)

                        ' Extract text replacing unmapped characters with correct Unicode values
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

            Dim pathToFile As String = "FixGarbledText.txt"
            Using writer = New StreamWriter(pathToFile)
                writer.Write(documentText.ToString())
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Function ocrCharacterCodes(charCodes As List(Of PdfCharacterCode), engine As TesseractEngine) As String()
            Dim recognizedText As String() = New String(charCodes.Count - 1) {}

            Dim rasterizer = New PdfTextRasterizer With {
                .HorizontalResolution = 300,
                .VerticalResolution = 300,
                .Height = 20,
                .CharacterSpacing = 1.5 ' Slightly increase distance between characters to improve OCR quality
            }

            ' Split character codes to batches because Tesseract cannot process too wide images.
            ' You may find the appropriate batch size heuristically based on the output image height and resolution.
            ' Or you may rasterize all character codes with the PdfTextRasterizer.Save method and
            ' calculate the batch size using the returning widths.
            Const BatchSize As Integer = 400
            Dim batchIndex As Integer = 0
            For Each batchCodes In charCodes.Chunk(BatchSize)
                Using charCodeImage = New MemoryStream()
                    ' Get bounds of rendered character codes
                    Dim widthsPoints As Double() = rasterizer.Save(charCodeImage, batchCodes)
                    Dim positionsPoints As Double() = New Double(widthsPoints.Length - 1) {}

                    For j As Integer = 1 To positionsPoints.Length - 1
                        positionsPoints(j) = positionsPoints(j - 1) + widthsPoints(j - 1) + rasterizer.CharacterSpacing
                    Next

                    ' Perform OCR
                    Using img As Pix = Pix.LoadFromMemory(charCodeImage.ToArray())
                        ' Use SingleChar segmentation mode when the last batch contains a few characters
                        Dim segMode As PageSegMode = If(batchCodes.Length > 5, engine.DefaultPageSegMode, PageSegMode.SingleChar)
                        Using recognizedPage As Page = engine.Process(img, segMode)
                            Using iter As ResultIterator = recognizedPage.GetIterator()
                                ' Map character codes to the recognized text
                                Dim lastCharCodeIndex As Integer = -1
                                Const Level As PageIteratorLevel = PageIteratorLevel.Symbol
                                iter.Begin()

                                Do
                                    Dim bounds As Rect = Nothing
                                    If iter.TryGetBoundingBox(Level, bounds) Then
                                        Dim bestIntersectionWidth As Double = 0
                                        Dim bestMatchIndex As Integer = -1

                                        For c As Integer = lastCharCodeIndex + 1 To batchCodes.Length - 1
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
                                            recognizedText(batchIndex * BatchSize + bestMatchIndex) = iter.GetText(Level)
                                            lastCharCodeIndex = bestMatchIndex
                                        End If
                                    End If
                                Loop While iter.[Next](Level)
                            End Using
                        End Using
                    End Using
                End Using

                batchIndex += 1
            Next

            Return recognizedText
        End Function

        Private Shared Function pointsToPixels(points As Double, dpi As Double) As Double
            Return points * dpi / 72
        End Function
    End Class
End Namespace