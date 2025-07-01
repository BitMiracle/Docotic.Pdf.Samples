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

                ' By default, the following call uses fonts installed on the operating system.
                ' To customize the font source, provide a font loader via PdfConfigurationOptions.
                ' For details, see: https : //api.docotic.com/pdfconfigurationoptions
                Dim systemFont As PdfFont = pdf.CreateFont("Arial", False, True)
                canvas.Font = systemFont

                Dim stringOptions As New PdfStringDrawingOptions With {
                    .Strikethrough = True
                }
                canvas.DrawString(10, 50, "Hello, world!", stringOptions)

                ' The following call creates a built-in font. These fonts do Not increase file
                ' size, as they are included with PDF viewers And don't need to be embedded in the
                ' document.
                Dim builtInFont As PdfFont = pdf.CreateFont(PdfBuiltInFont.TimesRoman)
                canvas.Font = builtInFont

                canvas.DrawString(10, 70, "Hello, world!")

                Dim fontFromFile As PdfFont = pdf.CreateFontFromFile("..\Sample data\Fonts\HolidayPi_BT.ttf")
                canvas.Font = fontFromFile

                Dim rect As New PdfRectangle(10, 90, 100, 100)
                Dim textOptions As New PdfTextDrawingOptions(rect) With {
                    .Underline = True
                }
                canvas.DrawText("Hello world", textOptions)

                ' To ensure consistent appearance across devices, the library embeds all fonts
                ' that are Not built-in. To reduce output file size, you can remove unused glyphs
                ' from the embedded fonts.
                systemFont.RemoveUnusedGlyphs()
                fontFromFile.RemoveUnusedGlyphs()

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace