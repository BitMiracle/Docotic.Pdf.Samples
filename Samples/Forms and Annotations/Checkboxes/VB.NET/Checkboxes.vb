Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class Checkboxes
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "Checkboxes.pdf"

            Using pdf As New PdfDocument()

                Dim page As PdfPage = pdf.Pages(0)
                page.AddCheckBox(10, 50, 120, 15, "Check box")

                Dim readonlyCheckBox As PdfCheckBox = page.AddCheckBox(10, 100, 120, 15, "Read-only check box")
                readonlyCheckBox.[ReadOnly] = True
                readonlyCheckBox.Checked = True

                Dim checkBoxWithBorder As PdfCheckBox = page.AddCheckBox(10, 150, 140, 15, "Check box with colored border")
                checkBoxWithBorder.Border.Color = New PdfCmykColor(40, 90, 70, 3)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace