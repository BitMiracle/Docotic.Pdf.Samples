Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ColorProfiles
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = Pdf.Pages(0).Canvas

                Dim colorProfile As PdfColorProfile = pdf.AddColorProfile("..\Sample data\AdobeRGB1998.icc")
                canvas.Brush.Color = New PdfRgbColor(colorProfile, 20, 80, 240)
                canvas.DrawRectangle(New PdfRectangle(10, 50, 100, 70), PdfDrawMode.Fill)

                pdf.Save("ColorProfiles.pdf")
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace