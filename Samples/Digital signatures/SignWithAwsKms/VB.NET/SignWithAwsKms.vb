Imports System.Security.Cryptography.X509Certificates
Imports Amazon.KeyManagementService
Imports Amazon.KeyManagementService.Model
Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignWithAwsKms
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await SignAsync()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function SignAsync() As Task
            ' IMPORTANT:
            ' Configure AWS credentials in advance (for example, using AWS CLI).
            ' And change these variables to use desired key id And signing algorithm.
            Const KeyId As String = "arn:aws:kms:YOUR-id"
            Dim signingAlgorithm = SigningAlgorithmSpec.RSASSA_PSS_SHA_512
            Dim key As GetPublicKeyResponse

            Using client = New AmazonKeyManagementServiceClient()
                Dim getPublicKeyRequest = New GetPublicKeyRequest() With {
                    .KeyId = KeyId
                }
                key = Await client.GetPublicKeyAsync(getPublicKeyRequest)

                If Not key.SigningAlgorithms.Contains(signingAlgorithm.ToString()) Then
                    Console.WriteLine($"Unsupported signing algorithm: {signingAlgorithm}")
                    Return
                End If
            End Using

            Dim signer = New AwsSigner(KeyId, signingAlgorithm)

            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim outputFileName As String = "SignWithAzureKeyVault.pdf"
            Using pdf = New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)
                Dim field As PdfSignatureField = page.AddSignatureField(50, 50, 200, 200)

                ' This sample automatically generates self-signed certificate for the provided AWS key.
                ' In real applications, you might want to use a CA signed certificate.
                Using cert As X509Certificate2 = AwsCertificateGenerator.Generate(key, signingAlgorithm)
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
        End Function
    End Class
End Namespace