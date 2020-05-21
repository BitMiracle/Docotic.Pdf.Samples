Imports System.IO
Imports System.Text
Imports Tesseract

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class OcrAndExtractText
        Public Shared Sub Main()
            ' NOTE 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim documentText = New StringBuilder()
            Using pdf = New PdfDocument("Sample data/Freedman Scora.pdf")

                Using engine = New TesseractEngine("tessdata", "eng", EngineMode.LstmOnly)

                    For i As Integer = 0 To pdf.PageCount - 1
                        If documentText.Length > 0 Then documentText.Append(vbCrLf & vbCrLf)
                        Dim page As PdfPage = pdf.Pages(i)
                        Dim searchableText As String = page.GetText()

                        If Not String.IsNullOrEmpty(searchableText.Trim()) Then
                            documentText.Append(searchableText)
                            Continue For
                        End If

                        Dim options As PdfDrawOptions = PdfDrawOptions.Create()
                        options.BackgroundColor = New PdfRgbColor(255, 255, 255)
                        options.HorizontalResolution = 200
                        options.VerticalResolution = 200
                        Dim pageImage As String = $"page_{i}.png"
                        page.Save(pageImage, options)

                        Using img As Pix = Pix.LoadFromFile(pageImage)
                            Using recognizedPage As Page = engine.Process(img)
                                Console.WriteLine($"Mean confidence for page #{i}: {recognizedPage.GetMeanConfidence()}")

                                Dim recognizedText As String = recognizedPage.GetText()
                                documentText.Append(recognizedText)
                            End Using
                        End Using

                        File.Delete(pageImage)
                    Next
                End Using
            End Using

            Const Result As String = "result.txt"
            Using writer = New StreamWriter(Result)
                writer.Write(documentText.ToString())
            End Using

            Process.Start(Result)
        End Sub
    End Class
End Namespace