Imports System.Diagnostics
Imports System.Drawing

Imports BitMiracle.Docotic.Pdf
Imports BitMiracle.Docotic.Pdf.Gdi

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class DrawPageOnGraphics
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToImage As String = "DrawPageOnGraphics.png"

            Using pdf As New PdfDocument("Sample Data\jfif3.pdf")
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

            Process.Start(pathToImage)
        End Sub
    End Class
End Namespace