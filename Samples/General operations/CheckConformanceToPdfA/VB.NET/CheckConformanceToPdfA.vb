Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CheckConformanceToPdfA
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Using pdf As New PdfDocument("..\Sample Data\PDF-A.pdf")
                Dim level As PdfaConformance = pdf.GetPdfaConformance()
                Console.WriteLine("PDF/A conformance level: " + level.ToString())
            End Using
        End Sub
    End Class
End Namespace
