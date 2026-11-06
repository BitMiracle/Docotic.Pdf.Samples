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
                    pdf.Pages.Add()
                Next

                ' Use lowercase Roman numerals (i, ii, iii, iv) for the first four pages.
                pdf.PageLabels.Add(0, 3, PdfPageNumberingStyle.LowercaseRoman)

                ' Use decimal Arabic numerals (1, 2, 3) for the next three pages.
                pdf.PageLabels.Add(4, PdfPageNumberingStyle.DecimalArabic)

                ' Use the labels A-8, A-9, and A-10 for the next three pages.
                pdf.PageLabels.Add(7, PdfPageNumberingStyle.DecimalArabic, "A-", 8)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
