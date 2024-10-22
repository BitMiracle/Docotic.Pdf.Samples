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

                ' add a page to the end and then delete it
                pdf.AddPage()
                pdf.RemovePage(pdf.PageCount - 1)

                ' another way to delete a page
                Dim pageToDelete As PdfPage = pdf.AddPage()
                pdf.RemovePage(pageToDelete)

                ' we can delete several pages at once
                pdf.InsertPage(0)
                pdf.InsertPage(1)
                pdf.RemovePages(New Integer() {0, 1})

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace