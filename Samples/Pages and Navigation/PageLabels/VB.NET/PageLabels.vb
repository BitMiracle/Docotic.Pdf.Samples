Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class PageLabels
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "PageLabels.pdf"

            Using pdf As New PdfDocument()

                For i As Integer = 0 To 8
                    pdf.AddPage()
                Next

                ' first four pages will have labels i, ii, iii, and iv
                pdf.PageLabels.AddRange(0, 3, PdfPageNumberingStyle.LowercaseRoman)

                ' next three pages will have labels 1, 2, and 3
                pdf.PageLabels.AddRange(4, PdfPageNumberingStyle.DecimalArabic)

                ' next three pages will have labels A-8, A-9 and A-10
                pdf.PageLabels.AddRange(7, PdfPageNumberingStyle.DecimalArabic, "A-", 8)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace
