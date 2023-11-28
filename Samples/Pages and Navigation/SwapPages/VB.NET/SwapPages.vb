Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SwapPages
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "SwapPages.pdf"

            Using pdf As New PdfDocument()

                BuildTestDocument(pdf)

                pdf.SwapPages(9, 0)
                pdf.SwapPages(8, 1)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Sub BuildTestDocument(pdf As PdfDocument)
            Dim times As PdfFont = pdf.AddFont(PdfBuiltInFont.HelveticaBoldOblique)
            Dim pageWidth As Double = pdf.GetPage(0).Width

            Dim page As PdfPage = pdf.GetPage(0)
            For i As Integer = 0 To 9
                If i > 0 Then
                    page = pdf.AddPage()
                End If

                page.Canvas.Font = times
                page.Canvas.FontSize = 16

                Dim titleFormat As String = String.Format("Page {0}", i + 1)

                Dim textWidth As Double = page.Canvas.GetTextWidth(titleFormat)
                page.Canvas.DrawString(New PdfPoint((pageWidth - textWidth) / 2, 100), titleFormat)
            Next
        End Sub
    End Class
End Namespace
