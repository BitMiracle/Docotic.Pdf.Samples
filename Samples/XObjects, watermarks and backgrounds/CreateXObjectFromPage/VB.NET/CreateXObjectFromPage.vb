Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CreateXObjectFromPage
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "CreateXObjectFromPage.pdf"

            Using other As New PdfDocument("..\Sample Data\jfif3.pdf")
                Using pdf As New PdfDocument()
                    Dim firstXObject As PdfXObject = pdf.CreateXObject(other.Pages(0))
                    Dim secondXObject As PdfXObject = pdf.CreateXObject(other.Pages(1))

                    Dim page As PdfPage = pdf.Pages(0)
                    Dim halfOfPage As Double = page.Width / 2
                    pdf.Pages(0).Canvas.DrawXObject(firstXObject, 0, 0, halfOfPage, 400, 0)
                    pdf.Pages(0).Canvas.DrawXObject(secondXObject, halfOfPage, 0, halfOfPage, 400, 0)

                    pdf.Save(pathToFile)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace