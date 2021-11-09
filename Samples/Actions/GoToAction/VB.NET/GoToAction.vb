Imports System.Diagnostics
Imports System.IO

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class GoToAction
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf As New PdfDocument()

                Dim secondPage As PdfPage = pdf.AddPage()
                secondPage.Canvas.DrawString(10, 300, "Go-to action target")

                Dim action As PdfGoToAction = pdf.CreateGoToPageAction(1, 300)
                Dim annotation As PdfActionArea = pdf.Pages(0).AddActionArea(10, 50, 100, 30, action)
                annotation.Border.Color = New PdfRgbColor(255, 0, 0)
                annotation.Border.DashPattern = New PdfDashPattern(New Single() {3, 2})
                annotation.Border.Width = 1
                annotation.Border.Style = PdfMarkerLineStyle.Dashed

                pdf.Save("GoToAction.pdf")
            End Using

            Process.Start("GoToAction.pdf")
        End Sub
    End Class
End Namespace