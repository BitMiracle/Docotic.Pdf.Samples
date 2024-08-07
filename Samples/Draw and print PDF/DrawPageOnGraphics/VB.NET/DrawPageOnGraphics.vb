Imports System.Drawing
Imports System.Runtime.Versioning
Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf
Imports BitMiracle.Docotic.Pdf.Gdi

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class DrawPageOnGraphics
        <SupportedOSPlatform("windows")>
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToImage As String = "DrawPageOnGraphics.png"

            Using pdf As New PdfDocument("..\Sample Data\jfif3.pdf")
                Const TargetResolution As Single = 300

                Dim page As PdfPage = pdf.Pages(0)
                Dim scaleFactor As Double = TargetResolution / page.Resolution

                Using bitmap As New Bitmap(CInt(page.Width * scaleFactor), CInt(page.Height * scaleFactor))
                    bitmap.SetResolution(TargetResolution, TargetResolution)

                    Using gr As Graphics = Graphics.FromImage(bitmap)
                        page.Draw(gr)
                    End Using

                    bitmap.Save(pathToImage)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToImage) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace