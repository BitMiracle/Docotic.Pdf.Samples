Imports Azure.Security.KeyVault.Keys.Cryptography
Imports BitMiracle.Docotic.Pdf
Imports AzureSignatureAlgorithm = Azure.Security.KeyVault.Keys.Cryptography.SignatureAlgorithm

Class AzureSigner
    Implements IPdfSigner

    Private ReadOnly m_client As CryptographyClient
    Private ReadOnly m_signingAlgorithm As AzureSignatureAlgorithm

    Public Sub New(client As CryptographyClient, signingAlgorithm As AzureSignatureAlgorithm)
        m_client = client
        m_signingAlgorithm = signingAlgorithm
    End Sub

    Public ReadOnly Property SignatureAlgorithm As PdfSignatureAlgorithm Implements IPdfSigner.SignatureAlgorithm
        Get
            If m_signingAlgorithm = AzureSignatureAlgorithm.ES256 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.ES384 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.ES512 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.ES256K Then
                Return PdfSignatureAlgorithm.Ecdsa
            End If

            If m_signingAlgorithm = AzureSignatureAlgorithm.PS256 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.PS384 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.PS512 Then
                Return PdfSignatureAlgorithm.RsaSsaPss
            End If

            If m_signingAlgorithm = AzureSignatureAlgorithm.RS256 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.RS384 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.RS512 Then
                Return PdfSignatureAlgorithm.Rsa
            End If

            Throw New InvalidOperationException($"Unsupported {NameOf(SignatureAlgorithm)} value: {m_signingAlgorithm}")
        End Get
    End Property

    Public ReadOnly Property DigestAlgorithm As PdfDigestAlgorithm
        Get
            If m_signingAlgorithm = AzureSignatureAlgorithm.RS256 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.PS256 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.ES256 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.ES256K Then
                Return PdfDigestAlgorithm.Sha256
            End If

            If m_signingAlgorithm = AzureSignatureAlgorithm.RS384 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.PS384 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.ES384 Then
                Return PdfDigestAlgorithm.Sha384
            End If

            If m_signingAlgorithm = AzureSignatureAlgorithm.RS512 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.PS512 OrElse
                m_signingAlgorithm = AzureSignatureAlgorithm.ES512 Then
                Return PdfDigestAlgorithm.Sha512
            End If

            Throw New InvalidOperationException($"Unsupported {NameOf(SignatureAlgorithm)} value: {m_signingAlgorithm}")
        End Get
    End Property

    Public Function Sign(message As Byte()) As Byte() Implements IPdfSigner.Sign
        Return m_client.SignData(m_signingAlgorithm, message).Signature
    End Function
End Class
