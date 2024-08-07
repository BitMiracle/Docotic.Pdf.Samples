Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class FindControlByName
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Const pathToFile As String = "FindControlByName.pdf"

            Using pdf As New PdfDocument("..\Sample Data\form.pdf")
                Dim emailTextBox As PdfTextBox = TryCast(pdf.GetControl("email"), PdfTextBox)
                If emailTextBox IsNot Nothing Then
                    emailTextBox.Text = "support@bitmiracle.com"
                End If

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace