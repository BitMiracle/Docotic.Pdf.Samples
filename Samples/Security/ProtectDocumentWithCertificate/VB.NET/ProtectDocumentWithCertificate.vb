Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ProtectDocumentWithCertificate
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "ProtectDocumentWithCertificate.pdf"

            Using pdf As New PdfDocument()
                pdf.Pages(0).Canvas.DrawString("Hello World!")

                Dim handler = CreateEncryptionHandler()
                handler.Algorithm = PdfEncryptionAlgorithm.Aes256Bit

                Dim saveOptions As New PdfSaveOptions With {.EncryptionHandler = handler}
                pdf.Save(pathToFile, saveOptions)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub

        Private Shared Function CreateEncryptionHandler() As PdfPublicKeyEncryptionHandler
            ' TODO:
            ' Change the constants, or the sample won't work.

            Dim keyStoreOwner As String = "key-store-owner.p12"
            Dim passwordOwner As String = "password"

            Dim keyStoreUser As String = "key-store-user.p12"
            Dim passwordUser As String = "password"

            ' You can also use an X509Certificate2 certificate to construct the handler
            Dim handler As New PdfPublicKeyEncryptionHandler(keyStoreOwner, passwordOwner)

            ' Add another recipient with non-owner permissions
            Dim permissions As New PdfPermissions With {
                .Flags = PdfPermissionFlags.FillFormFields Or PdfPermissionFlags.PrintDocument
            }
            handler.AddRecipient(keyStoreUser, passwordUser, permissions)

            Return handler
        End Function
    End Class
End Namespace