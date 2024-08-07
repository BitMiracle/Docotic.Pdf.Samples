Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class AddPages
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "AddPages.pdf"

            Using pdf As New PdfDocument()
                ' There is already one page in PDF document after creation
                Dim firstPage As PdfPage = pdf.Pages(0)
                firstPage.Canvas.DrawString("This is the auto-created first page")

                ' AddPage method adds a new page to the end of the PDF document
                Dim secondPage As PdfPage = pdf.AddPage()
                secondPage.Canvas.DrawString("Second page")

                ' You can insert a new page in an arbitrary place
                Dim newFirstPage As PdfPage = pdf.InsertPage(0)
                newFirstPage.Canvas.DrawString("New first page")

                ' This call is an equivalent of AddPage method
                Dim lastPage As PdfPage = pdf.InsertPage(pdf.PageCount)
                lastPage.Canvas.DrawString("Last page")

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace