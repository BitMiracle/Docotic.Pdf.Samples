Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class WordSpacing
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "WordSpacing.pdf"

            Using pdf As New PdfDocument()

                Const sampleText As String = "Lorem ipsum dolor sit amet, consectetur adipisicing elit"
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                canvas.WordSpacing = 1
                canvas.DrawString(10, 50, sampleText)

                canvas.WordSpacing = 4
                canvas.DrawString(10, 60, sampleText)

                canvas.WordSpacing = 10
                canvas.DrawString(10, 70, sampleText)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace