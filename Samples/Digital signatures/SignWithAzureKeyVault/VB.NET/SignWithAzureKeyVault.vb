Imports Azure.Identity
Imports Azure.Security.KeyVault.Keys
Imports Azure.Security.KeyVault.Keys.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignWithAzureKeyVault
        Public Shared Sub Main()
            ' IMPORTANT:
            ' Change these variables using your vault url, key name, signing algorithm, and Azure credentials.
            Const VaultUrl As String = "https://YOUR-VAULT.vault.azure.net/"
            Const KeyName As String = "YOUR-KEY"
            Dim signingAlgorithm = SignatureAlgorithm.RS512
            Dim credentials = New AzureCliCredential()

            Dim keyClient = New KeyClient(vaultUri:=New Uri(VaultUrl), credential:=credentials)
            Dim key As KeyVaultKey = keyClient.GetKey(KeyName)
            Dim cryptoClient = New CryptographyClient(key.Id, credentials)
            Dim signer = New AzureSigner(cryptoClient, signingAlgorithm)

            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.
            Dim outputFileName As String = "SignWithAzureKeyVault.pdf"
            Using pdf = New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)
                Dim field As PdfSignatureField = page.AddSignatureField(50, 50, 200, 200)

                ' This sample automatically generates self-signed certificate for the provided Azure key.
                ' In real applications, you might want to use a CA signed certificate.
                Using cert As X509Certificate2 = AzureCertificateGenerator.Generate(key, cryptoClient, signingAlgorithm)
                    Dim options = New PdfSigningOptions(signer, {cert}) With {
                        .DigestAlgorithm = signer.DigestAlgorithm,
                        .Field = field
                    }

                    pdf.SignAndSave(options, outputFileName)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo(outputFileName) With {
                .UseShellExecute = True
            })
        End Sub
    End Class
End Namespace