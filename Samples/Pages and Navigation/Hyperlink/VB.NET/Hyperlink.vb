Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Hyperlink
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "Hyperlink.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)
                Dim drawStringOptions As New PdfStringDrawingOptions With {
                    .AddActionAreas = True
                }
                page.Canvas.DrawString(10, 50, "Url: http://bitmiracle.com", drawStringOptions)

                Dim rectWithLink As New PdfRectangle(10, 70, 200, 100)
                page.Canvas.DrawRectangle(rectWithLink, PdfDrawMode.Stroke)

                Dim drawTextOptions = New PdfTextDrawingOptions(rectWithLink) With
                {
                    .HorizontalAlignment = PdfTextAlign.Center,
                    .VerticalAlignment = PdfVerticalAlign.Center,
                    .AddActionAreas = True
                }
                page.Canvas.DrawText("Go to Google", drawTextOptions)
                page.AddHyperlink(rectWithLink, New Uri("http://google.com"))

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace