Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CheckConformanceToPdfA
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Using pdf As New PdfDocument("..\Sample Data\PDF-A.pdf")
                Dim level As PdfaConformance = pdf.GetPdfaConformance()
                Console.WriteLine("PDF/A conformance level: " + level.ToString())
            End Using
        End Sub
    End Class
End Namespace
