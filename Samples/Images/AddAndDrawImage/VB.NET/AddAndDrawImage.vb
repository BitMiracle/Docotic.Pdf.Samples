Imports System.Diagnostics
Imports System.Drawing
Imports System.IO

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class AddAndDrawImage
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.
            Dim pathToFile As String = "AddAndDrawImage.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                Dim image As PdfImage = pdf.AddImage("Sample data/ammerland.jpg")
                canvas.DrawImage(image, 10, 50)

                ' NOTE: PdfDocument.AddImage(System.Drawing.Image) method is not supported in version for .NET Standard
                Using bitmap As New Bitmap("Sample data/pink.png")
                    Dim image2 As PdfImage = pdf.AddImage(bitmap)
                    canvas.DrawImage(image2, 400, 50, 100, 150, -45)
                End Using

                pdf.Save(pathToFile)
            End Using

            Process.Start(pathToFile)
        End Sub
    End Class
End Namespace
