Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Shapes
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "Shapes.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                canvas.DrawCircle(New PdfPoint(60, 100), 40)
                canvas.DrawEllipse(New PdfRectangle(10, 150, 100, 50))
                canvas.DrawRectangle(New PdfRectangle(160, 80, 110, 50))
                canvas.DrawRoundedRectangle(New PdfRectangle(160, 150, 110, 50), New PdfSize(30, 30))

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace