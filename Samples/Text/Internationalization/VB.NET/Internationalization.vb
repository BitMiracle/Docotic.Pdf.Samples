Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Internationalization
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
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

                Dim nsimsun = pdf.AddFont("NSimSun")
                If nsimsun Is Nothing Then
                    Console.WriteLine("Cannot add NSimSun font")
                    Return
                End If

                canvas.Font = nsimsun
                canvas.DrawString(180, 50, "世界您好")

                Dim timesNewRoman = pdf.AddFont("Times New Roman")
                If timesNewRoman Is Nothing Then
                    Console.WriteLine("Cannot add Times New Roman font")
                    Return
                End If

                canvas.Font = timesNewRoman
                canvas.DrawString(180, 70, "Привет, мир")
                canvas.DrawString(180, 90, "Olá mundo")

                Dim options = New PdfStringDrawingOptions With {
                    .ReadingDirection = PdfReadingDirection.Rtl
                }
                canvas.DrawString(180, 110, "bidi בינלאומי", options)

                pdf.RemoveUnusedFontGlyphs()

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace