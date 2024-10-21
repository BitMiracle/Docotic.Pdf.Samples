Imports System.IO
Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class TypographySample
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Const PathToFile As String = "Typography.pdf"
            PdfDocumentBuilder.Create().Generate(PathToFile,
                Sub(doc)
                    doc.Typography(New CustomTypography(doc))

                    doc.Pages(
                        Sub(page)
                            page.MarginTop(50)

                            page.Header().Border(Sub(b) b.Bottom(1)).Text("Header")

                            page.Content().Text(
                                Sub(text)
                                    text.Line("Regular")

                                    text.Line("Title").Style(Function(t) t.Title)
                                    text.Line("SubTitle").Style(Function(t) t.SubTitle)
                                    text.Line("Heading1").Style(Function(t) t.Heading1)
                                    text.Line("Heading2").Style(Function(t) t.Heading2)
                                    text.Line("Heading3").Style(Function(t) t.Heading3)

                                    text.Line("Caption").Style(Function(t) t.Caption)
                                    text.Line("Footnote").Style(Function(t) t.Footnote)

                                    text.Hyperlink("Hyperlink", New Uri("https://bitmiracle.com"))
                                    text.Line()

                                    text.Line("Emphasis").Style(Function(t) t.Emphasis)
                                    text.Line("Strong").Style(Function(t) t.Strong)
                                    text.Line("printf(""Plain"");").Style(Function(t) t.Plain)

                                    text.Line("Sample").Style(Function(t) TryCast(t, CustomTypography).Sample)
                                End Sub)

                            page.Footer().Border(Sub(b) b.Top(1)).Text(
                                Sub(t)
                                    t.Span("Footer ")
                                    t.CurrentPageNumber()
                                End Sub)
                        End Sub)
                End Sub)

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class

    Class CustomTypography
        Inherits Typography
        Public Sub New(doc As Document)
            MyBase.New(doc)

            ' Use the custom font with the default document style
            Dim roboto = New FileInfo("..\Sample Data\Fonts\Roboto\Roboto-Regular.ttf")
            Document = doc.TextStyleWithFont(roboto)

            ' Customize the size of the page header font
            Header = Parent.FontSize(8)

            ' Customize the size and style of the page footer font
            Footer = Emphasis.FontSize(8)

            ' Create a custom style
            Sample = Parent.Strikethrough().Underline().FontColor(New PdfRgbColor(255, 0, 0))
        End Sub

        Public Sample As TextStyle
    End Class

End Namespace
