Imports System
Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class UriAction
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf As New PdfDocument()

                Dim uriAction As PdfUriAction = pdf.CreateHyperlinkAction(New Uri("http://www.google.com"))
                Dim annotation As PdfActionArea = pdf.Pages(0).AddActionArea(10, 50, 100, 100, uriAction)
                annotation.BorderColor = New PdfRgbColor(0, 0, 0)
                annotation.BorderDashPattern = New PdfDashPattern(New Single() {3, 2})

                pdf.Save("UriAction.pdf")
            End Using

            Process.Start("UriAction.pdf")
        End Sub
    End Class
End Namespace