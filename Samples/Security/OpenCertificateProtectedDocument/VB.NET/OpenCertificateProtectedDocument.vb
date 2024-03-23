Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class OpenCertificateProtectedDocument
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            ' TODO:
            ' Change the constants, Or the sample won't work.
            Dim encryptedFile As String = "certificate-encrypted.pdf"
            Dim keyStore As String = "key-store.p12"
            Dim password As String = "password"

            OpenWithAutoSelectedCertificate(encryptedFile)
            OpenWithKeyStore(encryptedFile, keyStore, password)

            ' There are other options. You can use a X509Store Or X509Certificate2
            ' to construct a PdfPublicKeyDecryptionHandler And then use it 
            ' to open a certificate protected document.
        End Sub

        Private Shared Sub OpenWithAutoSelectedCertificate(encryptedFile As String)
            Try
                ' This will only work if a matching certificate is installed in the
                ' X.509 certificate store used by the current user
                Using pdf As New PdfDocument(encryptedFile)
                    Console.WriteLine(
                        String.Format(
                        "Opened with an auto-selected certificate from the current user certificate store. " &
                        "Permissions = {0}", pdf.GrantedPermissions))
                End Using
            Catch ex As PdfException
                Console.WriteLine(ex.ToString())
            End Try
        End Sub

        Private Shared Sub OpenWithKeyStore(encryptedFile As String, keyStore As String, password As String)
            Try
                Dim handler As New PdfPublicKeyDecryptionHandler(keyStore, password)

                Using pdf As New PdfDocument(encryptedFile, handler)
                    Console.WriteLine(
                        String.Format(
                        "Opened with a certificate from the key store at {0}. Permissions = {1}",
                        keyStore, pdf.GrantedPermissions))
                End Using
            Catch ex As PdfException
                Console.WriteLine(ex.ToString())
            End Try
        End Sub
    End Class
End Namespace