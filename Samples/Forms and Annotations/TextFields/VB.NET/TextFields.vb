Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class TextFields
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "TextFields.pdf"

            Using pdf As New PdfDocument()

                Dim page As PdfPage = pdf.Pages(0)
                Dim textBox As PdfTextBox = page.AddTextBox(10, 50, 100, 30)
                textBox.Text = "Hello"

                Dim textBoxWithMaxLength As PdfTextBox = page.AddTextBox(10, 90, 100, 30)
                textBoxWithMaxLength.MaxLength = 5

                Dim passwordTextBox As PdfTextBox = page.AddTextBox(10, 130, 100, 30)
                passwordTextBox.Text = "Secret password"
                passwordTextBox.PasswordField = True

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace