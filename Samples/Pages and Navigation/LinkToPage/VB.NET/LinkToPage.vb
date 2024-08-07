Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class LinkToPage
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "LinkToPage.pdf"

            Using pdf As New PdfDocument()
                pdf.AddPage()
                pdf.AddPage()

                Dim firstPage As PdfPage = pdf.Pages(0)
                Dim canvas As PdfCanvas = firstPage.Canvas
                Dim rectForLinkToSecondPage As New PdfRectangle(10, 50, 100, 60)
                canvas.DrawRectangle(rectForLinkToSecondPage, PdfDrawMode.Stroke)
                DrawCenteredText(canvas, "Go to 2nd page", rectForLinkToSecondPage)
                firstPage.AddLinkToPage(rectForLinkToSecondPage, 1)

                Dim rectForLinkToThirdPage As New PdfRectangle(150, 50, 100, 60)
                canvas.DrawRectangle(rectForLinkToThirdPage, PdfDrawMode.Stroke)
                DrawCenteredText(canvas, "Go to 3rd page", rectForLinkToThirdPage)
                firstPage.AddLinkToPage(rectForLinkToThirdPage, pdf.Pages(2))

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Sub DrawCenteredText(canvas As PdfCanvas, text As String, bounds As PdfRectangle)
            Dim options = New PdfTextDrawingOptions(bounds) With
            {
                .HorizontalAlignment = PdfTextAlign.Center,
                .VerticalAlignment = PdfVerticalAlign.Center
            }
            canvas.DrawText(text, options)
        End Sub
    End Class
End Namespace