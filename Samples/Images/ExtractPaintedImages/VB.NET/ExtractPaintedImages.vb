Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ExtractPaintedImages
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf As New PdfDocument("Sample Data\ImageScaleAndRotate.pdf")
                Dim paintedImages As PdfCollection(Of PdfPaintedImage) = pdf.Pages(0).GetPaintedImages()

                Dim image As PdfPaintedImage = paintedImages(0)

                ' save image as is
                Dim imageAsIs As String = "PdfImage.Save"
                Dim fullPath As String = image.Image.Save(imageAsIs)
                Process.Start(fullPath)

                ' save image as painted
                Dim imageAsPainted As String = "PdfPaintedImage.SaveAsPainted.tiff"
                image.SaveAsPainted(imageAsPainted, PdfExtractedImageFormat.Tiff)
                Process.Start(imageAsPainted)
            End Using
        End Sub
    End Class
End Namespace