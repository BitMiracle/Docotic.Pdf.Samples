Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ProtectDocumentWithCertificate
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "ProtectDocumentWithCertificate.pdf"

            Using pdf As New PdfDocument()
                pdf.Pages(0).Canvas.DrawString("Hello World!")

                Dim handler = createEncryptionHandler()
                handler.Algorithm = PdfEncryptionAlgorithm.Aes256Bit

                Dim saveOptions As New PdfSaveOptions With {.EncryptionHandler = handler}
                pdf.Save(pathToFile, saveOptions)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub

        Private Shared Function createEncryptionHandler() As PdfPublicKeyEncryptionHandler
            ' TODO: 
            ' Change the constants, or the sample won't work.

            Dim keyStoreOwner As String = "key-store-owner.p12"
            Dim passwordOwner As String = "password"

            Dim keyStoreUser As String = "key-store-user.p12"
            Dim passwordUser As String = "password"

            ' You can also use an X509Certificate2 certificate to construct the handler
            Dim handler As PdfPublicKeyEncryptionHandler = New PdfPublicKeyEncryptionHandler(keyStoreOwner, passwordOwner)

            ' Add another recipient with non-owner permissions
            Dim permissions As PdfPermissions = New PdfPermissions()
            permissions.Flags = PdfPermissionFlags.FillFormFields Or PdfPermissionFlags.PrintDocument
            handler.AddRecipient(keyStoreUser, passwordUser, permissions)

            Return handler
        End Function
    End Class
End Namespace