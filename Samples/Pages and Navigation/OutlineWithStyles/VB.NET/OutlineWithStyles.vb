Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class OutlineWithStyles
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "OutlineWithStyles.pdf"

            Using pdf As New PdfDocument()
                pdf.PageMode = PdfPageMode.UseOutlines
                pdf.PageLayout = PdfPageLayout.OneColumn

                BuildTestOutline(pdf)

                Dim outlines = pdf.Outlines
                Dim makeBold = True
                For Each child In outlines
                    If makeBold Then
                        child.Bold = True
                    Else
                        child.Italic = True
                    End If
                    makeBold = Not makeBold

                    Dim makeGreen = True
                    For Each childOfChild In child.Children
                        If makeGreen Then
                            childOfChild.Color = New PdfRgbColor(0, 100, 0) ' dark green
                        Else
                            childOfChild.Color = New PdfRgbColor(138, 43, 226) ' blue violet
                        End If
                        makeGreen = Not makeGreen

                        childOfChild.Bold = True
                        childOfChild.Italic = True
                    Next
                Next

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Sub BuildTestOutline(pdf As PdfDocument)
            Dim lastParent As PdfOutlineItem = Nothing

            Dim times As PdfFont = pdf.CreateFont(PdfBuiltInFont.TimesItalic)
            Dim pageWidth As Single = pdf.Pages(0).Width

            Dim page As PdfPage = pdf.Pages(pdf.Pages.Count - 1)
            For i As Integer = 1 To 29
                If i > 1 Then
                    page = pdf.Pages.Add()
                End If

                page.Canvas.Font = times
                page.Canvas.FontSize = 16

                Dim titleFormat As String = String.Format("Page {0}", i)

                Dim textWidth As Single = page.Canvas.GetTextWidth(titleFormat)
                page.Canvas.DrawString(New PdfPoint((pageWidth - textWidth) / 2, 100), titleFormat)

                Dim view = pdf.CreateView(i - 1)

                If i = 1 OrElse i = 10 OrElse i = 20 Then
                    lastParent = pdf.Outlines.Add(titleFormat)
                    lastParent.View = view
                Else
                    Dim child = lastParent.Children.Add(titleFormat)
                    child.View = view
                End If
            Next
        End Sub
    End Class
End Namespace
