Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Transparency
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "Transparency.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                Dim rect As New PdfRectangle(50, 50, 50, 50)

                canvas.Brush.Opacity = 100
                canvas.Pen.Opacity = 100
                canvas.Brush.Color = New PdfRgbColor(200, 140, 7)
                canvas.DrawRectangle(rect, 0, PdfDrawMode.Fill)

                canvas.Brush.Opacity = 40
                canvas.Brush.Color = New PdfRgbColor(125, 125, 255)
                rect.Location = New PdfPoint(rect.X + 15, rect.Y + 15)
                canvas.DrawRectangle(rect, 0, PdfDrawMode.Fill)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace