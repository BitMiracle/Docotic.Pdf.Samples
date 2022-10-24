Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class RemoveWidgets
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "RemoveWidgets.pdf"

            Using pdf As New PdfDocument("..\Sample Data\form.pdf")
                pdf.Pages(0).Widgets.RemoveAt(1)
                pdf.Save("RemoveWidgetFromPage.pdf")

                For Each widget As PdfWidget In pdf.GetWidgets()
                    pdf.RemoveWidget(widget)
                Next

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
