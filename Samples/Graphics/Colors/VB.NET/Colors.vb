Imports System.Diagnostics
Imports System.Drawing

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Colors
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "Colors.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                canvas.Brush.Color = New PdfRgbColor(255, 0, 0)
                canvas.Pen.Color = New PdfRgbColor(0, 255, 255)
                canvas.Pen.Width = 3

                canvas.DrawEllipse(New RectangleF(10, 50, 200, 100), PdfDrawMode.FillAndStroke)

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace