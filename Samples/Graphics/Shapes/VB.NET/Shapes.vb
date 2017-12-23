Imports System.Diagnostics
Imports System.Drawing

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Shapes
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf As New PdfDocument()

                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                canvas.DrawCircle(New PointF(60, 100), 40)
                canvas.DrawEllipse(New RectangleF(10, 150, 100, 50))
                canvas.DrawRectangle(New RectangleF(160, 80, 110, 50))
                canvas.DrawRoundedRectangle(New RectangleF(160, 150, 110, 50), New SizeF(30, 30))

                pdf.Save("Shapes.pdf")
            End Using

            Process.Start("Shapes.pdf")
        End Sub
    End Class
End Namespace