Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class OpenCertificateProtectedDocument
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

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