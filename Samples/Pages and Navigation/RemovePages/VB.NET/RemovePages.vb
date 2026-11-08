Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class RemovePages
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "RemovePages.pdf"

            Using pdf As New PdfDocument()

                Dim firstPage As PdfPage = pdf.Pages(0)
                firstPage.Canvas.DrawString(10, 50, "Main page")

                ' Add a page at the end, then remove it.
                pdf.Pages.Add()
                pdf.Pages.RemoveAt(pdf.Pages.Count - 1)

                ' Remove multiple pages at once.
                pdf.Pages.Insert(0)
                pdf.Pages.Insert(1)
                pdf.Pages.RemoveAt(New Integer() {0, 1})

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace