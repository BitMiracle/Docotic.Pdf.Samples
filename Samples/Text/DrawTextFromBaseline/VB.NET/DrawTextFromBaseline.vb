Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class DrawTextFromBaseline
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "DrawTextFromBaseline.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                canvas.FontSize = 30
                canvas.Font = pdf.CreateFont(PdfBuiltInFont.TimesItalic)

                ' draw a straight line across the page
                Const baselinePosition As Double = 100
                canvas.CurrentPosition = New PdfPoint(0.0, baselinePosition)
                canvas.DrawLineTo(New PdfPoint(pdf.Pages(0).Width, baselinePosition))

                ' calculate the distance from the baseline to the top of the font bounding box
                Dim distanceToBaseLine As Double = GetDistanceToBaseline(canvas.Font, canvas.FontSize)

                ' draw text so that the baseline of the text is exactly over the straight line
                canvas.DrawString(10, baselinePosition - distanceToBaseLine, "gyQW")

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Function GetDistanceToBaseline(font As PdfFont, fontSize As Double) As Double
            Return font.TopSideBearing * font.TransformationMatrix.M22 * fontSize
        End Function
    End Class
End Namespace