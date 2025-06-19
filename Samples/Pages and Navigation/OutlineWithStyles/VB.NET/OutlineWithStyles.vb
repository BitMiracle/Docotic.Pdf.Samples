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

                Dim root As PdfOutlineItem = pdf.OutlineRoot

                For i As Integer = 0 To root.ChildCount - 1
                    Dim child As PdfOutlineItem = root.GetChild(i)

                    If i Mod 2 = 0 Then
                        child.Bold = True
                    Else
                        child.Italic = True
                    End If

                    For j As Integer = 0 To child.ChildCount - 1
                        Dim childOfChild As PdfOutlineItem = child.GetChild(j)

                        If j Mod 2 = 0 Then
                            childOfChild.Color = New PdfRgbColor(0, 100, 0) ' dark green
                        Else
                            childOfChild.Color = New PdfRgbColor(138, 43, 226) ' blue violet
                        End If

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
            Dim root As PdfOutlineItem = pdf.OutlineRoot
            Dim lastParent As PdfOutlineItem = Nothing

            Dim times As PdfFont = pdf.CreateFont(PdfBuiltInFont.TimesItalic)
            Dim pageWidth As Single = pdf.GetPage(0).Width

            Dim page As PdfPage = pdf.GetPage(pdf.PageCount - 1)
            For i As Integer = 1 To 29
                If i > 1 Then
                    page = pdf.AddPage()
                End If

                page.Canvas.Font = times
                page.Canvas.FontSize = 16

                Dim titleFormat As String = String.Format("Page {0}", i)

                Dim textWidth As Single = page.Canvas.GetTextWidth(titleFormat)
                page.Canvas.DrawString(New PdfPoint((pageWidth - textWidth) / 2, 100), titleFormat)

                Dim action As PdfGoToAction = pdf.CreateGoToPageAction(i - 1, 0)

                If i = 1 OrElse i = 10 OrElse i = 20 Then
                    lastParent = root.AddChild(titleFormat, action)
                Else
                    lastParent.AddChild(titleFormat, action)
                End If
            Next
        End Sub
    End Class
End Namespace
