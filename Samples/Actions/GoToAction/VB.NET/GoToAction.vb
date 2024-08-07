Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class GoToAction
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "GoToAction.pdf"

            Using pdf As New PdfDocument()

                Dim secondPage As PdfPage = pdf.AddPage()
                secondPage.Canvas.DrawString(10, 300, "Go-to action target")

                Dim action As PdfGoToAction = pdf.CreateGoToPageAction(1, 300)
                Dim annotation As PdfActionArea = pdf.Pages(0).AddActionArea(10, 50, 100, 30, action)
                annotation.Border.Color = New PdfRgbColor(255, 0, 0)
                annotation.Border.DashPattern = New PdfDashPattern(New Single() {3, 2})
                annotation.Border.Width = 1
                annotation.Border.Style = PdfMarkerLineStyle.Dashed

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace