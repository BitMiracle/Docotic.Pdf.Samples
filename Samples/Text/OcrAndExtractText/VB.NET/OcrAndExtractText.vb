Imports System.IO
Imports System.Reflection
Imports System.Text
Imports BitMiracle.Docotic.Pdf
Imports Tesseract

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class OcrAndExtractText
        Public Shared Sub Main()
            ' NOTE 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim documentText = New StringBuilder()
            Using pdf = New PdfDocument("..\Sample data\Freedman Scora.pdf")
                Dim location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                If location Is Nothing Then
                    Console.WriteLine("Invalid assembly location")
                    Return
                End If

                Dim tessData = Path.Combine(location, "tessdata")
                Using engine = New TesseractEngine(tessData, "eng", EngineMode.LstmOnly)
                    For i As Integer = 0 To pdf.PageCount - 1
                        If documentText.Length > 0 Then documentText.Append(vbCrLf & vbCrLf)

                        Dim page As PdfPage = pdf.Pages(i)
                        Dim searchableText As String = page.GetText()

                        ' Simple check if the page contains searchable text.
                        ' We do not need to perform OCR in that case.
                        If Not String.IsNullOrEmpty(searchableText.Trim()) Then
                            documentText.Append(searchableText)
                            Continue For
                        End If

                        ' This page is not searchable.
                        ' Save PDF page as a high-resolution image.
                        Dim options As PdfDrawOptions = PdfDrawOptions.Create()
                        options.BackgroundColor = New PdfRgbColor(255, 255, 255)
                        options.HorizontalResolution = 200
                        options.VerticalResolution = 200

                        Dim pageImage As String = $"page_{i}.png"
                        page.Save(pageImage, options)

                        ' Perform OCR
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

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(Result) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace