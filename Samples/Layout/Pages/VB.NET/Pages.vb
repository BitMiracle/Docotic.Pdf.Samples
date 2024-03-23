Imports System.IO
Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class Pages
        Public Shared Sub Main()
            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Const PathToFile As String = "Pages.pdf"
            PdfDocumentBuilder.Create().Generate(PathToFile,
                Sub(doc)
                    doc.Pages(
                        Sub(page)
                            page.Size(PdfPaperSize.A4, PdfPaperOrientation.Portrait).
                                MarginVertical(50).
                                MarginHorizontal(40).
                                BackgroundColor(New PdfGrayColor(95))

                            Dim watermarkColor = New PdfRgbColor(255, 0, 0)
                            page.Foreground().
                                Extend().
                                AlignCenter().
                                AlignMiddle().
                                TranslateX(50).
                                TranslateY(200).
                                Rotate(-55).
                                Text("Watermark").
                                Style(Function(t) t.Parent.FontSize(100).FontColor(watermarkColor, 50))

                            page.Header().
                                PaddingBottom(30).
                                AlignRight().
                                Text("Header")

                            Dim loremIpsum As String = File.ReadAllText("..\Sample Data\lorem-ipsum.txt")
                            page.Content().Text(loremIpsum)

                            page.Footer().
                                AlignCenter().
                                Text(Sub(t) t.CurrentPageNumber())
                        End Sub)

                    doc.Pages(
                        Sub(page)
                            page.Size(PdfPaperSize.A3, PdfPaperOrientation.Landscape).
                                MarginVertical(50).
                                ContentFromRightToLeft().
                                Content().
                                Text("Hello World")
                        End Sub)

                    doc.Pages(
                    Sub(page)
                        page.Size(100, 100).
                            Content().
                            Text("Custom page size")
                    End Sub)
                End Sub)

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
