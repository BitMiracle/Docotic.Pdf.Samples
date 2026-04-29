Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf
Imports BitMiracle.Docotic.Pdf.PdfA

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ConvertPdfToPdfA
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "ConvertPdfToPdfA.pdf"

            Using pdf As New PdfDocument("..\Sample Data\gmail-cheat-sheet.pdf")
                pdf.SaveAsPdfa(pathToFile, PdfaConformanceLevel.Pdfa4)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace