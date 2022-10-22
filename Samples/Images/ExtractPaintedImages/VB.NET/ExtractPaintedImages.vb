Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ExtractPaintedImages
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim asIsOutputPath As String = ""
            Dim asPaintedOutputPath As String = "PdfPaintedImage.SaveAsPainted.tiff"

            Using pdf As New PdfDocument("..\Sample Data\ImageScaleAndRotate.pdf")
                Dim paintedImages As PdfCollection(Of PdfPaintedImage) = pdf.Pages(0).GetPaintedImages()

                Dim image As PdfPaintedImage = paintedImages(0)

                ' save image as is
                asIsOutputPath = image.Image.Save("PdfImage.Save")

                ' save image as painted
                Dim options = New PdfPaintedImageSavingOptions With
                {
                    .Format = PdfExtractedImageFormat.Tiff,
                    .HorizontalResolution = 300,
                    .VerticalResolution = 300
                }
                image.SaveAsPainted(asPaintedOutputPath, options)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(asIsOutputPath) With {.UseShellExecute = True})
            Process.Start(New ProcessStartInfo(asPaintedOutputPath) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace