Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CopyPages
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf As New PdfDocument("Sample Data\jfif3.pdf")

                ' copy third and first pages to a new PDF document (page indexes are zero-based)
                Using copy As PdfDocument = pdf.CopyPages(New Integer() {2, 0})
                    copy.Save("CopyPages.pdf")
                End Using

            End Using

            Process.Start("CopyPages.pdf")
        End Sub
    End Class
End Namespace