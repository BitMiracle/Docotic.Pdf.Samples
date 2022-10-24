Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Listboxes
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "Listboxes.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                Dim listBox As PdfListBox = page.AddListBox(10, 50, 120, 80)
                listBox.AddItem("First item")
                listBox.AddItem("Second item")
                listBox.AddItem("Third item")
                listBox.MultiSelection = True

                Dim items As PdfListItem() = {New PdfListItem("One", "1"), New PdfListItem("Two", "2"), New PdfListItem("Three", "3")}
                Dim listBox2 As PdfListBox = page.AddListBox(10, 140, 120, 80)
                listBox2.AddItems(items)
                listBox2.SelectItems(New PdfListItem() {items(0), items(2)})

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
