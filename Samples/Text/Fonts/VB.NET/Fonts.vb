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

                Dim systemFont As PdfFont = pdf.AddFont("Arial", False, True, False, True)
                If systemFont Is Nothing Then
                    Console.WriteLine("Cannot add font")
                    Return
                End If

                canvas.Font = systemFont
                canvas.DrawString(10, 50, "Hello, world!")

                Dim builtInFont As PdfFont = pdf.AddFont(PdfBuiltInFont.TimesRoman)
                canvas.Font = builtInFont
                canvas.DrawString(10, 70, "Hello, world!")

                Dim fontFromFile As PdfFont = pdf.AddFontFromFile("..\Sample data\Fonts\HolidayPi_BT.ttf")
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