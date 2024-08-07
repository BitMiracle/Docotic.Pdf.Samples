Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Comboboxes
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "Comboboxes.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                Dim comboBox As PdfComboBox = page.AddComboBox(10, 50, 120, 15)
                comboBox.AddItem("First item")
                comboBox.AddItem("Second item")
                comboBox.AddItem("Third item")
                comboBox.Border.Color = New PdfRgbColor(255, 0, 0)
                comboBox.Text = "Second item"

                Dim comboBox2 As PdfComboBox = page.AddComboBox(10, 80, 120, 15)
                comboBox2.AddItem("First item")
                comboBox2.AddItem("Second item")
                comboBox2.HasEditField = False

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace