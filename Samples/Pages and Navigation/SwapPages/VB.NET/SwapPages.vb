Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SwapPages
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "SwapPages.pdf"

            Using pdf As New PdfDocument()

                BuildTestDocument(pdf)

                pdf.Pages.Swap(9, 0)
                pdf.Pages.Swap(8, 1)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Sub BuildTestDocument(pdf As PdfDocument)
            Dim times As PdfFont = pdf.CreateFont(PdfBuiltInFont.HelveticaBoldOblique)
            Dim page As PdfPage = pdf.Pages(0)
            Dim pageWidth As Double = page.Width

            For i As Integer = 0 To 9
                If i > 0 Then
                    page = pdf.Pages.Add()
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
