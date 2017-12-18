Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class PageLayout
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "PageLayout.pdf"

            Using pdf As New PdfDocument("Sample data/jfif3.pdf")
                pdf.PageLayout = PdfPageLayout.TwoColumnLeft

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace