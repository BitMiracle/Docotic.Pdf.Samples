Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class DrawText
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "DrawText.pdf"

            Using pdf As New PdfDocument()
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas

                canvas.DrawString(10, 50, "Hello, world!")

                Const LongString As String = "Lorem ipsum dolor sit amet, consectetur adipisicing elit"
                Dim singleLineOptions = New PdfTextDrawingOptions(New PdfRectangle(10, 70, 40, 150)) With
                {
                    .Multiline = False,
                    .HorizontalAlignment = PdfTextAlign.Left,
                    .VerticalAlignment = PdfVerticalAlign.Top
                }
                canvas.DrawText(LongString, singleLineOptions)

                Dim multiLineOptions = New PdfTextDrawingOptions(New PdfRectangle(70, 70, 40, 150)) With
                {
                    .HorizontalAlignment = PdfTextAlign.Left,
                    .VerticalAlignment = PdfVerticalAlign.Top
                }
                canvas.DrawText(LongString, multiLineOptions)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
