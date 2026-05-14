Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf
Imports BitMiracle.Docotic.Pdf.Conformance

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ReadPdfaConformance
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Using pdf As New PdfDocument("..\Sample Data\PDF-A.pdf")
                Dim level As PdfaConformanceLevel? = pdf.ReadPdfaConformance()
                If level.HasValue Then
                    Console.WriteLine("PDF/A conformance level: " + level.ToString())
                Else
                    Console.WriteLine("No PDF/A conformance level")
                End If
            End Using
        End Sub
    End Class
End Namespace
