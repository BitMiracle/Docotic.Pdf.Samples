Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class DrawTextFromBaseline
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "DrawTextFromBaseline.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                canvas.FontSize = 30
                canvas.Font = pdf.AddFont(PdfBuiltInFont.TimesItalic)

                ' draw straight line across the page
                Const baselinePosition As Double = 100
                canvas.CurrentPosition = New PdfPoint(0.0, baselinePosition)
                canvas.DrawLineTo(New PdfPoint(pdf.Pages(0).Width, baselinePosition))

                ' calculate distance from the baseline to the top of the font bounding box
                Dim distanceToBaseLine As Double = getDistanceToBaseline(canvas.Font, canvas.FontSize)

                ' draw text so that baseline of the text placed exactly over the straight line
                canvas.DrawString(10, baselinePosition - distanceToBaseLine, "gyQW")

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Function getDistanceToBaseline(font As PdfFont, fontSize As Double) As Double
            Return font.TopSideBearing * font.TransformationMatrix.M22 * fontSize
        End Function
    End Class
End Namespace