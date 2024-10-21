Imports System.IO
Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class Fonts
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Const PathToFile As String = "Fonts.pdf"
            PdfDocumentBuilder.
                Create().
                FontLoader(New DirectoryFontLoader(New String() {"..\Sample Data\Fonts"}, True)).
                MissingGlyphHandler(Function(text) "?").
                Generate(PathToFile,
                    Sub(doc)
                        ' Load the font using the current font loader
                        Dim roboto As TextStyle = doc.TextStyleWithFont(SystemFont.Family("Roboto").Bold())

                        ' Or provide the file path / stream explicitly
                        Dim path = New FileInfo("..\Sample Data\Fonts\Algerian.ttf")
                        Dim holiday As TextStyle = doc.TextStyleWithFont(path)

                        Dim hebrew As TextStyle = doc.TextStyleWithFont(SystemFont.Family("Noto Sans Hebrew"), FontEmbedMode.EmbedAllGlyphs)

                        doc.Pages(
                            Sub(page)
                                page.TextStyle(holiday.Fallback(roboto)).
                                    MarginTop(50).
                                    Content().
                                    Text(
                                        Sub(t)
                                            t.Line("Regular text")

                                            t.Line("Fallback: ₩Ѻ")

                                            t.Line("Missing glyph handler: 💁👌🎍😍")

                                            t.Line("RTL text: השעה").Style(hebrew)
                                        End Sub)
                            End Sub)
                    End Sub)

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
