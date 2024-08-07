Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class RadioButtons
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "RadioButtons.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                page.Canvas.DrawString(10, 50, "Docotic.Pdf library is written using: ")

                Dim radioButton As PdfRadioButton = page.AddRadioButton("languages", 10, 60, 100, 15, "C#")
                radioButton.ExportValue = "csharp"
                radioButton.Checked = True

                Dim radioButton2 As PdfRadioButton = page.AddRadioButton("languages", 10, 80, 100, 15, "Python")
                radioButton2.ExportValue = "python"
                radioButton2.Checked = False

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace