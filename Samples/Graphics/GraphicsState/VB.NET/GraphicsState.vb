Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class GraphicsState
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "GraphicsState.pdf"

            Using pdf As New PdfDocument()

                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                canvas.SaveState()

                canvas.Pen.Width = 3
                canvas.Pen.DashPattern = New PdfDashPattern(New Single() {5, 3}, 2)

                canvas.CurrentPosition = New PdfPoint(20, 60)
                canvas.DrawLineTo(150, 60)

                canvas.RestoreState()

                canvas.CurrentPosition = New PdfPoint(20, 80)
                canvas.DrawLineTo(150, 80)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace