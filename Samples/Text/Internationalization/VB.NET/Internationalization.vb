Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Internationalization
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "Internationalization.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                canvas.FontSize = 12

                canvas.Font = pdf.CreateFont(PdfBuiltInFont.CourierBold)
                canvas.DrawString(10, 50, "Chinese(traditional): ")
                canvas.DrawString(10, 70, "Russian: ")
                canvas.DrawString(10, 90, "Portugal: ")
                canvas.DrawString(10, 110, "Bidirectional: ")

                canvas.Font = pdf.CreateFont("NSimSun")
                canvas.DrawString(180, 50, "世界您好")

                canvas.Font = pdf.CreateFont("Times New Roman")
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