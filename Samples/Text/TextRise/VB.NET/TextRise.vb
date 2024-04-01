Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class TextRise
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "TextRise.pdf"

            Using pdf As New PdfDocument()

                Const sampleText As String = "Hello!"
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                canvas.TextPosition = New PdfPoint(10, 50)

                canvas.TextRise = 0
                canvas.DrawString(sampleText)
                canvas.TextRise = 5
                canvas.DrawString(sampleText)
                canvas.TextRise = -10
                canvas.DrawString(sampleText)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace