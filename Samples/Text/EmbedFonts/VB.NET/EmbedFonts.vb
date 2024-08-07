Imports System.IO
Imports BitMiracle.Docotic

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class EmbedFonts
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim outputPath As String = "EmbedFonts.pdf"

            Const FilePath As String = "..\Sample Data\non-embedded-font.pdf"

            Dim config As PdfConfigurationOptions = PdfConfigurationOptions.Create()
            config.FontLoader = New DirectoryFontLoader({"..\Sample Data\Fonts"}, False)

            Using pdf = New PdfDocument(FilePath, config)

                For Each font As PdfFont In pdf.GetFonts()
                    font.Embed()
                Next

                pdf.Save(outputPath)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(outputPath) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace