Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Clipping
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "Clipping.pdf"

            Using pdf As New PdfDocument()

                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                canvas.Pen.Width = 2
                canvas.Brush.Color = New PdfRgbColor(255, 0, 0)

                DrawStar(canvas)

                canvas.SetClip(PdfFillMode.Alternate)
                canvas.StrokePath()

                canvas.DrawRectangle(New PdfRectangle(0, 0, 500, 500), PdfDrawMode.Fill)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Sub DrawStar(canvas As PdfCanvas)
            canvas.CurrentPosition = New PdfPoint(10, 100)
            canvas.AppendLineTo(110, 100)
            canvas.AppendLineTo(30, 160)
            canvas.AppendLineTo(60, 70)
            canvas.AppendLineTo(80, 160)
            canvas.ClosePath()
        End Sub
    End Class
End Namespace