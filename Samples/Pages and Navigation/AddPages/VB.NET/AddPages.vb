Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class AddPages
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "AddPages.pdf"

            Using pdf As New PdfDocument()
                ' There is already one page in PDF document after creation
                Dim firstPage As PdfPage = Pdf.Pages(0)
                firstPage.Canvas.DrawString("This is the auto-created first page")

                ' AddPage method adds a new page to the end of the PDF document
                Dim secondPage As PdfPage = Pdf.AddPage()
                secondPage.Canvas.DrawString("Second page")

                ' You can insert a new page in an arbitrary place
                Dim newFirstPage As PdfPage = Pdf.InsertPage(0)
                newFirstPage.Canvas.DrawString("New first page")

                ' This call is an equivalent of AddPage method
                Dim lastPage As PdfPage = Pdf.InsertPage(Pdf.PageCount)
                lastPage.Canvas.DrawString("Last page")

                Pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace