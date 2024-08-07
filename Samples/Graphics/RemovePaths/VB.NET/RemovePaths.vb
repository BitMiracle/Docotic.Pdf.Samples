Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class RemovePaths
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Const PathToFile As String = "RemovePaths.pdf"

            Using pdf = New PdfDocument("..\Sample Data\form.pdf")
                Dim page As PdfPage = pdf.Pages(0)
                page.RemovePaths(
                    Function(path)
                        ' remove the form header filled with orange (255, 127, 64)
                        If path.PaintMode = PdfDrawMode.Stroke Then Return False

                        Dim fillColor As PdfRgbColor = TryCast(path.Brush.Color, PdfRgbColor)
                        Return (
                            fillColor IsNot Nothing AndAlso
                            fillColor.R = 255 AndAlso
                            fillColor.G = 127 AndAlso
                            fillColor.B = 64
                        )
                    End Function
                )
                pdf.Save(PathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace