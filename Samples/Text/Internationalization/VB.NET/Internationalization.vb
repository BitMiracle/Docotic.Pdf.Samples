Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Internationalization
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "Internationalization.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                canvas.FontSize = 12

                canvas.Font = pdf.AddFont(PdfBuiltInFont.CourierBold)
                canvas.DrawString(10, 50, "Chinese(traditional): ")
                canvas.DrawString(10, 70, "Russian: ")
                canvas.DrawString(10, 90, "Portugal: ")
                canvas.DrawString(10, 110, "Bidirectional: ")

                canvas.Font = pdf.AddFont("NSimSun")
                canvas.DrawString(180, 50, "世界您好")

                canvas.Font = pdf.AddFont("Times New Roman")
                canvas.DrawString(180, 70, "Привет, мир")
                canvas.DrawString(180, 90, "Olá mundo")

                Dim options = New PdfStringDrawingOptions
                options.ReadingDirection = PdfReadingDirection.Rtl
                canvas.DrawString(180, 110, "bidi בינלאומי", options)

                pdf.RemoveUnusedFontGlyphs()

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace