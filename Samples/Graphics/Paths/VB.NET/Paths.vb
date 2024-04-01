Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Paths
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "Paths.pdf"

            Using pdf As New PdfDocument()

                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                canvas.Pen.Width = 3
                canvas.Pen.Color = New PdfRgbColor(78, 55, 150)
                canvas.Brush.Color = New PdfRgbColor(200, 55, 50)

                canvas.AppendRectangle(New PdfRectangle(10, 60, 100, 100))
                canvas.ResetPath()
                ' following call resets the path and rectangle won't be drawn
                canvas.CurrentPosition = New PdfPoint(20, 70)
                canvas.AppendLineTo(100, 70)
                canvas.AppendLineTo(100, 110)
                canvas.AppendCurveTo(New PdfPoint(80, 90), New PdfPoint(50, 90), New PdfPoint(30, 110))
                canvas.FillAndStrokePath(PdfFillMode.Winding)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace