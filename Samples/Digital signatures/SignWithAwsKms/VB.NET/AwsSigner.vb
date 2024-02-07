Imports System.IO
Imports Amazon.KeyManagementService
Imports Amazon.KeyManagementService.Model
Imports BitMiracle.Docotic.Pdf

Class AwsSigner
    Implements IPdfSigner

    Private ReadOnly m_keyId As String
    Private ReadOnly m_signingAlgorithm As SigningAlgorithmSpec

    Public Sub New(keyId As String, signingAlgorithm As SigningAlgorithmSpec)
        If signingAlgorithm = SigningAlgorithmSpec.SM2DSA Then
            Throw New ArgumentException("SM2DSA is not supported", NameOf(signingAlgorithm))
        End If

        m_keyId = keyId
        m_signingAlgorithm = signingAlgorithm
    End Sub

    Public ReadOnly Property SignatureAlgorithm As PdfSignatureAlgorithm Implements IPdfSigner.SignatureAlgorithm
        Get
            If m_signingAlgorithm = SigningAlgorithmSpec.ECDSA_SHA_256 OrElse
                m_signingAlgorithm = SigningAlgorithmSpec.ECDSA_SHA_384 OrElse
                m_signingAlgorithm = SigningAlgorithmSpec.ECDSA_SHA_512 Then
                Return PdfSignatureAlgorithm.Ecdsa
            End If

            If m_signingAlgorithm = SigningAlgorithmSpec.RSASSA_PKCS1_V1_5_SHA_256 OrElse
                m_signingAlgorithm = SigningAlgorithmSpec.RSASSA_PKCS1_V1_5_SHA_384 OrElse
                m_signingAlgorithm = SigningAlgorithmSpec.RSASSA_PKCS1_V1_5_SHA_512 Then
                Return PdfSignatureAlgorithm.Rsa
            End If

            If m_signingAlgorithm = SigningAlgorithmSpec.RSASSA_PSS_SHA_256 OrElse
                m_signingAlgorithm = SigningAlgorithmSpec.RSASSA_PSS_SHA_384 OrElse
                m_signingAlgorithm = SigningAlgorithmSpec.RSASSA_PSS_SHA_512 Then
                Return PdfSignatureAlgorithm.RsaSsaPss
            End If

            Throw New InvalidOperationException($"Unsupported {NameOf(SigningAlgorithmSpec)} value: {m_signingAlgorithm}")
        End Get
    End Property

    Public ReadOnly Property DigestAlgorithm As PdfDigestAlgorithm
        Get
            Dim alg As String = m_signingAlgorithm.Value
            If alg.EndsWith("256") Then Return PdfDigestAlgorithm.Sha256
            If alg.EndsWith("384") Then Return PdfDigestAlgorithm.Sha384
            If alg.EndsWith("512") Then Return PdfDigestAlgorithm.Sha512

            Throw New InvalidOperationException($"Unsupported {NameOf(SigningAlgorithmSpec)} value: {m_signingAlgorithm}")
        End Get
    End Property

    Public Function Sign(message As Byte()) As Byte() Implements IPdfSigner.Sign
        Using kmsClient = New AmazonKeyManagementServiceClient()
            Using stream = New MemoryStream(message)
                Dim signRequest = New SignRequest() With {
                    .SigningAlgorithm = m_signingAlgorithm,
                    .KeyId = m_keyId,
                    .MessageType = MessageType.RAW,
                    .Message = stream
                }
                Dim signResponse As SignResponse = kmsClient.SignAsync(signRequest).GetAwaiter().GetResult()
                Return signResponse.Signature.ToArray()
            End Using
        End Using
    End Function
End Class