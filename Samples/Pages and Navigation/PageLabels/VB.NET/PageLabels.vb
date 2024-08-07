Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class PageLabels
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

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

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
