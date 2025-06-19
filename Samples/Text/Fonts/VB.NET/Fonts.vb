Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Fonts
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "Fonts.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                Dim systemFont As PdfFont = pdf.CreateFont("Arial", False, True)
                canvas.Font = systemFont

                Dim options As New PdfStringDrawingOptions With {
                    .Strikethrough = True
                }
                canvas.DrawString(10, 50, "Hello, world!", options)

                Dim builtInFont As PdfFont = pdf.CreateFont(PdfBuiltInFont.TimesRoman)
                canvas.Font = builtInFont
                canvas.DrawString(10, 70, "Hello, world!")

                Dim fontFromFile As PdfFont = pdf.CreateFontFromFile("..\Sample data\Fonts\HolidayPi_BT.ttf")
                canvas.Font = fontFromFile
                canvas.DrawString(10, 90, "Hello world")

                ' Remove unused glyphs from TrueType fonts to optimize the size of the output.
                systemFont.RemoveUnusedGlyphs()
                fontFromFile.RemoveUnusedGlyphs()

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace