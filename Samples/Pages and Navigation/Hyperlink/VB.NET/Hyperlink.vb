Imports System
Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Hyperlink
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "Hyperlink.pdf"

            Using pdf As New PdfDocument()
                pdf.AutoCreateUriActions = True

                Dim page As PdfPage = pdf.Pages(0)
                page.Canvas.DrawString(10, 50, "Url: http://bitmiracle.com")

                Dim rectWithLink As New PdfRectangle(10, 70, 200, 100)
                page.Canvas.DrawRectangle(rectWithLink, PdfDrawMode.Stroke)
                page.Canvas.DrawString("Go to Google", rectWithLink, PdfTextAlign.Center, PdfVerticalAlign.Center)
                page.AddHyperlink(rectWithLink, New Uri("http://google.com"))

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace