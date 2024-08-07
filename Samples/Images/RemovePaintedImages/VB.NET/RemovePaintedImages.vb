Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class RemovePaintedImages
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Const PathToFile As String = "RemovePaintedImages.pdf"

            Using pdf = New PdfDocument("..\Sample Data\ImageScaleAndRotate.pdf")
                Dim page As PdfPage = pdf.Pages(0)
                page.RemovePaintedImages(
                    Function(image)
                        ' remove rotated images
                        Dim m As PdfMatrix = image.TransformationMatrix
                        Return Math.Abs(m.M12) > 0.001 Or Math.Abs(m.M21) > 0.001
                    End Function
                )
                pdf.Save(PathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace