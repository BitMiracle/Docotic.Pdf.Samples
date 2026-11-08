Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class MovePages
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim continuousOutputPath As String = "MovePagesContinuous.pdf"
            Dim arbitraryOutputPath As String = "MovePagesArbitrary.pdf"

            ' Move a continuous range of pages.
            Using pdf As New PdfDocument()
                BuildTestDocument(pdf)

                ' Move the first five pages to the end of the document.
                pdf.Pages.MoveRange(0, 5, pdf.Pages.Count)

                pdf.Save(continuousOutputPath)
            End Using

            ' Move pages selected by their indexes.
            Using pdf As New PdfDocument()
                BuildTestDocument(pdf)

                Dim indexes As Integer() = New Integer() {0, 2, 4, 6, 8}

                ' Move the pages at the specified indexes to the end of the document.
                pdf.Pages.Move(indexes, pdf.Pages.Count)

                pdf.Save(arbitraryOutputPath)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(continuousOutputPath) With {.UseShellExecute = True})
            Process.Start(New ProcessStartInfo(arbitraryOutputPath) With {.UseShellExecute = True})
        End Sub

        Private Shared Sub BuildTestDocument(pdf As PdfDocument)
            Dim times As PdfFont = pdf.CreateFont(PdfBuiltInFont.HelveticaBoldOblique)
            Dim pageWidth As Double = pdf.Pages(0).Width

            Dim page As PdfPage = pdf.Pages(0)
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
