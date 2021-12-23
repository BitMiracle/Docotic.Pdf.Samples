Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class MovePages
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            ' This shows how to move continuous ranges of pages
            Using pdf As New PdfDocument()
                buildTestDocument(pdf)

                ' move first half of pages to the end of the document
                pdf.MovePages(0, 5, pdf.PageCount)

                pdf.Save("MovePagesContinuous.pdf")
            End Using

            ' This shows how to move arbitrary sets of pages
            Using pdf As New PdfDocument()
                buildTestDocument(pdf)

                Dim indexes As Integer() = New Integer() {0, 2, 4, 6, 8}

                ' move odd pages to the end of the document
                pdf.MovePages(indexes, pdf.PageCount)

                pdf.Save("MovePagesArbitrary.pdf")
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub

        Private Shared Sub buildTestDocument(pdf As PdfDocument)
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
