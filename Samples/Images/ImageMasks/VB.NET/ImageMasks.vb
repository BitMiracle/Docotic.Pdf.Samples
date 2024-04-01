Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ImageMasks
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "ImageMasks.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                Dim scale As Double = pdf.Pages(0).Resolution / 150
                canvas.ScaleTransform(scale, scale)

                canvas.Brush.Color = New PdfRgbColor(0, 255, 0)
                canvas.DrawRectangle(New PdfRectangle(50, 450, 1150, 150), PdfDrawMode.Fill)

                Dim image As PdfImage = pdf.AddImage("..\Sample data\pink.png", "..\Sample data\pinkMask.tif")
                If image Is Nothing Then
                    Console.WriteLine("Cannot add image")
                    Return
                End If

                canvas.DrawImage(image, 550, 200)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace