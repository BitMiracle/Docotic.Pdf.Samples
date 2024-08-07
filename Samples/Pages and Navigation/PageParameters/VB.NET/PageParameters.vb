Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class PageParameters
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "PageParameters.pdf"

            Using pdf As New PdfDocument()
                pdf.InitialView = pdf.CreateView(0)
                pdf.InitialView.SetFitPage()

                Dim firstPage As PdfPage = pdf.Pages(0)
                firstPage.Size = PdfPaperSize.A6

                Dim secondPage As PdfPage = pdf.AddPage()
                secondPage.Width = 300
                secondPage.Height = 100

                Dim thirdPage As PdfPage = pdf.AddPage()
                thirdPage.Rotation = PdfRotation.Rotate90

                Dim fourthPage As PdfPage = pdf.AddPage()
                fourthPage.Orientation = PdfPaperOrientation.Landscape

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace