Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class BlendModes
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "BlendModes.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                Dim location As New PdfPoint(50, 80)
                Dim size As New PdfSize(50, 50)

                Dim modes As PdfBlendMode() = {PdfBlendMode.Hue, PdfBlendMode.Lighten, PdfBlendMode.Darken}
                For i As Integer = 0 To modes.Length - 1
                    If i <> 0 Then
                        location.X += size.Width * 2
                    End If

                    canvas.BlendMode = PdfBlendMode.Normal

                    canvas.Brush.Color = New PdfRgbColor(0, 0, 0)
                    canvas.DrawString(location.X, location.Y - 20, "BlendMode: " & modes(i).ToString())

                    canvas.Brush.Color = New PdfRgbColor(200, 140, 7)
                    canvas.DrawRectangle(New PdfRectangle(location, size), 0, PdfDrawMode.Fill)

                    canvas.BlendMode = modes(i)
                    canvas.Brush.Color = New PdfRgbColor(125, 125, 255)
                    Dim secondRect As New PdfRectangle(location.X + 15, location.Y + 15, size.Width, size.Height)
                    canvas.DrawRectangle(secondRect, 0, PdfDrawMode.Fill)
                Next

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace