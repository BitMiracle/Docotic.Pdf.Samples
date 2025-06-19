Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ImageMasks
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "ImageMasks.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                Dim scale As Double = pdf.Pages(0).Resolution / 150
                canvas.ScaleTransform(scale, scale)

                canvas.Brush.Color = New PdfRgbColor(0, 255, 0)
                canvas.DrawRectangle(New PdfRectangle(50, 450, 1150, 150), PdfDrawMode.Fill)

                Dim maskImage As PdfImage = pdf.CreateImage("..\Sample data\pinkMask.tif")
                maskImage.ConvertToMask()

                Dim image As PdfImage = pdf.CreateImage("..\Sample data\pink.png")
                image.Mask = maskImage

                canvas.DrawImage(image, 550, 200)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace