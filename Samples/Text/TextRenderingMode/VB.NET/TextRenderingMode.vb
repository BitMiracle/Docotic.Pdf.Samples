Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class TextRenderingMode
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "TextRenderingMode.pdf"

            Using pdf As New PdfDocument()

                Const sampleText As String = "Lorem ipsum dolor sit amet, consectetur adipisicing elit"
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                canvas.Pen.Color = New PdfRgbColor(255, 0, 0)
                canvas.Brush.Color = New PdfGrayColor(70)
                canvas.FontSize = 20
                canvas.Pen.Width = 1

                canvas.TextRenderingMode = PdfTextRenderingMode.FillAndStroke
                canvas.DrawString(10, 50, sampleText)

                canvas.TextRenderingMode = PdfTextRenderingMode.Stroke
                canvas.DrawString(10, 70, sampleText)

                canvas.TextRenderingMode = PdfTextRenderingMode.Fill
                canvas.DrawString(10, 90, sampleText)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace