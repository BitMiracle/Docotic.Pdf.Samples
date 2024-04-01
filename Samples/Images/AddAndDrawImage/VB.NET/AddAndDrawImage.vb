Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class AddAndDrawImage
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.
            Dim pathToFile As String = "AddAndDrawImage.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                Dim image As PdfImage = pdf.AddImage("..\Sample data\ammerland.jpg")
                If image Is Nothing Then
                    Console.WriteLine("Cannot add image")
                    Return
                End If

                canvas.DrawImage(image, 10, 50)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
