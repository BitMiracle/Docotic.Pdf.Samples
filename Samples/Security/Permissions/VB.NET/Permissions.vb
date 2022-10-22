Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Permissions
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

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