Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ColorMasks
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "ColorMasks.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                Dim scale As Double = pdf.Pages(0).Resolution / 150
                canvas.ScaleTransform(scale, scale)

                canvas.Brush.Color = New PdfRgbColor(0, 255, 0)
                canvas.DrawRectangle(New PdfRectangle(50, 450, 1150, 150), PdfDrawMode.Fill)

                Dim image As PdfImage = pdf.AddImage("Sample data/pink.png", New PdfRgbColor(255, 0, 255))
                canvas.DrawImage(image, 550, 200)

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace