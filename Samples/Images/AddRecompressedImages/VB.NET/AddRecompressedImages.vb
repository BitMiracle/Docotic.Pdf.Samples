Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class AddRecompressedImages
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "AddRecompressedImages.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                Dim imageFrames As PdfImageFrames = pdf.OpenImage("..\Sample Data\pink.png")
                If imageFrames Is Nothing Then
                    Console.WriteLine("Cannot add image")
                    Return
                End If

                Dim originalImage As PdfImage = pdf.AddImage(imageFrames(0))
                canvas.DrawImage(originalImage, 10, 10, 0)

                Dim imageFramesToRecompress As PdfImageFrames = pdf.OpenImage("..\Sample Data\pink.png")
                If imageFramesToRecompress Is Nothing Then
                    Console.WriteLine("Cannot add image")
                    Return
                End If

                Dim frame As PdfImageFrame = imageFramesToRecompress(0)
                frame.OutputCompression = PdfImageCompression.Jpeg
                frame.JpegQuality = 50
                frame.RecompressAlways = True

                Dim compressedImage As PdfImage = pdf.AddImage(frame)
                canvas.DrawImage(compressedImage, 210, 10, 0)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace