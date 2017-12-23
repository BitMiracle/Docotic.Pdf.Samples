Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Clipping
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "Clipping.pdf"

            Using pdf As New PdfDocument()

                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                canvas.Pen.Width = 2
                canvas.Brush.Color = New PdfRgbColor(255, 0, 0)

                drawStar(canvas)

                canvas.SetClip(PdfFillMode.Alternate)
                canvas.StrokePath()

                canvas.DrawRectangle(New PdfRectangle(0, 0, 500, 500), PdfDrawMode.Fill)

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub

        Private Shared Sub drawStar(ByVal canvas As PdfCanvas)
            canvas.CurrentPosition = New PdfPoint(10, 100)
            canvas.AppendLineTo(110, 100)
            canvas.AppendLineTo(30, 160)
            canvas.AppendLineTo(60, 70)
            canvas.AppendLineTo(80, 160)
            canvas.ClosePath()
        End Sub
    End Class
End Namespace