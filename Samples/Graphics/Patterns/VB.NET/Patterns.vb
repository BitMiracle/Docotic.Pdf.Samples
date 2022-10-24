Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Patterns
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "Patterns.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                Dim helvetica As PdfFont = pdf.AddFont(PdfBuiltInFont.HelveticaBoldOblique, False, False)
                canvas.Font = helvetica
                canvas.FontSize = 28

                drawColoredPattern(pdf)
                drawUncoloredPattern(pdf)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Sub drawColoredPattern(ByVal pdf As PdfDocument)
            Dim pattern As PdfTilingPattern = pdf.AddColoredPattern(5, 5)
            fill(pattern)

            Dim canvas As PdfCanvas = pdf.GetPage(0).Canvas
            canvas.Brush.Pattern = pattern
            canvas.DrawString(New PdfPoint(50, 50), "Pattern-filled text")
            canvas.DrawCircle(New PdfPoint(0, 0), 20, PdfDrawMode.FillAndStroke)
        End Sub

        Private Shared Sub drawUncoloredPattern(ByVal pdf As PdfDocument)
            Dim pattern As PdfTilingPattern = pdf.AddUncoloredPattern(5, 5, New PdfRgbColorSpace())
            fill(pattern)

            Dim canvas As PdfCanvas = pdf.GetPage(0).Canvas
            canvas.Brush.Pattern = pattern
            canvas.Brush.Color = New PdfRgbColor(0, 255, 0)
            canvas.DrawString(New PdfPoint(50, 150), "Uncolored Pattern colored green")
        End Sub

        Private Shared Sub fill(ByVal pattern As PdfTilingPattern)
            Dim canvas As PdfCanvas = pattern.Canvas
            Dim red As PdfColor = New PdfRgbColor(255, 0, 0)
            canvas.Brush.Color = red
            canvas.Pen.Color = red
            canvas.AppendCircle(New PdfPoint(2, 2), 2)
            canvas.FillAndStrokePath(PdfFillMode.Winding)
        End Sub
    End Class
End Namespace