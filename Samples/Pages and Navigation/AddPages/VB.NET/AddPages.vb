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
                ' A new PDF document already contains one page
                Dim firstPage As PdfPage = pdf.Pages(0)
                firstPage.Canvas.DrawString("This is the auto-created first page")

                ' Add a new page at the end of the document
                Dim secondPage As PdfPage = pdf.Pages.Add()
                secondPage.Canvas.DrawString("Second page")

                ' Insert a new page at the specified position
                Dim newFirstPage As PdfPage = pdf.Pages.Insert(0)
                newFirstPage.Canvas.DrawString("New first page")

                ' Inserting at Count is equivalent to calling Add
                Dim lastPage As PdfPage = pdf.Pages.Insert(pdf.Pages.Count)
                lastPage.Canvas.DrawString("Last page")

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace