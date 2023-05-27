Imports System.IO
Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class TextStyling
        Public Shared Sub Main()
            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Const PathToFile As String = "TextStyling.pdf"
            PdfDocumentBuilder.Create().Generate(PathToFile,
                Sub(doc)
                    Dim root As TextStyle = TextStyle.Parent.
                        FontColor(New PdfRgbColor(255, 0, 0)).
                        FontSize(30)

                    Dim child As TextStyle = TextStyle.Parent.
                        FontColor(New PdfRgbColor(0, 0, 255)).
                        Strikethrough().
                        LetterSpacing(0.5)

                    Dim holidayFont = New FileInfo("../Sample Data/Fonts/HolidayPi_BT.ttf")
                    Dim custom As TextStyle = doc.TextStyleWithFont(holidayFont).
                        FontSize(20).
                        FontColor(New PdfRgbColor(255, 165, 0)).
                        BackgroundColor(New PdfGrayColor(90)).
                        LetterSpacing(3).
                        LineHeight(2)

                    doc.Pages(
                        Sub(page)
                            page.TextStyle(root).MarginTop(50)

                            page.Header().Text(
                                Sub(t)
                                    t.Span("Root style")
                                    t.Span("superscript").Style(TextStyle.Parent.Superscript())
                                End Sub)

                            page.Content().Text(
                                Sub(t)
                                    t.Style(child)

                                    t.Line("Child style")

                                    t.Line("Custom style").Style(custom.Strikethrough(False))
                                End Sub)
                        End Sub)
                End Sub)

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
