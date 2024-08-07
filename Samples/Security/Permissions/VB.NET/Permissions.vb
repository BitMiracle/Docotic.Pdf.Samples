Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Permissions
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "Permissions.pdf"

            Using pdf As New PdfDocument("..\Sample data\form.pdf")

                ' an owner password should be set in order to use user access permissions
                Dim handler = New PdfStandardEncryptionHandler("owner", "user")
                handler.UserPermissions.Flags = PdfPermissionFlags.None

                Dim saveOptions As New PdfSaveOptions With {.EncryptionHandler = handler}
                pdf.Save(pathToFile, saveOptions)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace