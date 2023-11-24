Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Fonts
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "Fonts.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                Dim systemFont As PdfFont = pdf.AddFont("Arial", False, True, False, True)
                canvas.Font = systemFont
                canvas.DrawString(10, 50, "Hello, world!")

                Dim builtInFont As PdfFont = pdf.AddFont(PdfBuiltInFont.TimesRoman)
                canvas.Font = builtInFont
                canvas.DrawString(10, 70, "Hello, world!")

                Dim fontFromFile As PdfFont = pdf.AddFontFromFile("..\Sample data\Fonts\HolidayPi_BT.ttf")
                canvas.Font = fontFromFile
                canvas.DrawString(10, 90, "Hello world")

                ' Remove unused glyphs from TrueType fonts to optimize size of result PDF file.
                systemFont.RemoveUnusedGlyphs()
                fontFromFile.RemoveUnusedGlyphs()

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace