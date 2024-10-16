Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class RemoveWidgets
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim removeFromPageOutputName As String = "RemoveWidgetFromPage.pdf"
            Dim removeAllOutputName As String = "RemoveWidgets.pdf"

            Using pdf As New PdfDocument("..\Sample Data\form.pdf")
                pdf.Pages(0).Widgets.RemoveAt(1)
                pdf.Save(removeFromPageOutputName)

                For Each widget As PdfWidget In pdf.GetWidgets().ToArray
                    pdf.RemoveWidget(widget)
                Next

                pdf.Save(removeAllOutputName)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(removeFromPageOutputName) With {.UseShellExecute = True})
            Process.Start(New ProcessStartInfo(removeAllOutputName) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
