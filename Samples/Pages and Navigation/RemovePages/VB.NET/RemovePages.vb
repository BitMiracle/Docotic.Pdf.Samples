Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class RemovePages
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "RemovePages.pdf"

            Using pdf As New PdfDocument()

                Dim firstPage As PdfPage = pdf.Pages(0)
                firstPage.Canvas.DrawString(10, 50, "Main page")

                'add page to the end and then delete it
                pdf.AddPage()
                pdf.RemovePage(pdf.PageCount - 1)

                'another way to delete page
                Dim pageToDelete As PdfPage = pdf.AddPage()
                pdf.RemovePage(pageToDelete)

                'we can delete several pages at once
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