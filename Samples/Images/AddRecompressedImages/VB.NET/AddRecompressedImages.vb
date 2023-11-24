Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class AddRecompressedImages
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

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