Imports System.IO

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class EmbedFonts
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const FilePath As String = "..\Sample Data\non-embedded-font.pdf"

            Dim config As PdfConfigurationOptions = PdfConfigurationOptions.Create()
            config.FontLoader = New DirectoryFontLoader({"..\Sample Data\Fonts"}, False)

            Using pdf = New PdfDocument(FilePath, config)

                For Each font As PdfFont In pdf.GetFonts()
                    font.Embed()
                Next

                pdf.Save("EmbedFonts.pdf")
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace