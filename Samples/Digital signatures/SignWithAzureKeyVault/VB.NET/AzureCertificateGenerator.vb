Imports Azure.Security.KeyVault.Keys
Imports Azure.Security.KeyVault.Keys.Cryptography
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates

Class AzureCertificateGenerator
    Inherits X509SignatureGenerator

    Private Const SubjectDn As String = "CN=self-generated cert,OU=Docotic.Pdf samples,O=Bit Miracle"
    Private Shared ReadOnly SerialNumber As Byte() = New Byte() {17, 59, 46}
    Private Shared ReadOnly NotBefore As DateTimeOffset = Date.UtcNow
    Private Shared ReadOnly NotAfter As DateTimeOffset = NotBefore.AddYears(2)

    Private ReadOnly m_baseGenerator As X509SignatureGenerator
    Private ReadOnly m_client As CryptographyClient
    Private ReadOnly m_signingAlgorithm As SignatureAlgorithm

    Private Sub New(client As CryptographyClient, alg As SignatureAlgorithm, baseGenerator As X509SignatureGenerator)
        m_baseGenerator = baseGenerator
        m_client = client
        m_signingAlgorithm = alg
    End Sub

    Public Shared Function Generate(key As KeyVaultKey, client As CryptographyClient, signingAlgorithm As SignatureAlgorithm) As X509Certificate2
        Return Generate(key, client, signingAlgorithm, SubjectDn, NotBefore, NotAfter, SerialNumber)
    End Function

    Public Shared Function Generate(
        key As KeyVaultKey,
        client As CryptographyClient,
        signingAlgorithm As SignatureAlgorithm,
        subjectDN As String,
        notBefore As DateTimeOffset,
        notAfter As DateTimeOffset,
        serialNumber As Byte()) As X509Certificate2

        Dim type = key.KeyType
        Dim hashAlg As HashAlgorithmName = GetHashAlgorithmName(signingAlgorithm.ToString())
        Dim certificateRequest As CertificateRequest
        Dim baseGenerator As X509SignatureGenerator

        If type = KeyType.Ec Then
            ' At the moment (7 Feb 2024), this generation of self-signed certificates for Azure KeyVault ECDSA keys
            ' does Not work. Use a certificate from KeyVault Secrets Or KeyVault Certificates instead.
            Dim ecdsa As ECDsa = key.Key.ToECDsa()
            certificateRequest = New CertificateRequest(subjectDN, ecdsa, hashAlg)
            baseGenerator = CreateForECDsa(ecdsa)
        ElseIf type = KeyType.Rsa Then
            Dim rsa As RSA = key.Key.ToRSA()
            Dim alg As String = signingAlgorithm.ToString()
            Dim padding As RSASignaturePadding = If(alg.StartsWith("RS"), RSASignaturePadding.Pkcs1, RSASignaturePadding.Pss)
            certificateRequest = New CertificateRequest(subjectDN, rsa, hashAlg, padding)
            baseGenerator = CreateForRSA(rsa, padding)
        Else
            Throw New ArgumentException("Cannot determine encryption algorithm for " & type.ToString(), NameOf(key))
        End If

        Dim generator = New AzureCertificateGenerator(client, signingAlgorithm, baseGenerator)
        Return certificateRequest.Create(New X500DistinguishedName(subjectDN), generator, notBefore, notAfter, serialNumber)
    End Function

    Public Overrides Function GetSignatureAlgorithmIdentifier(hashAlgorithm As HashAlgorithmName) As Byte()
        Return m_baseGenerator.GetSignatureAlgorithmIdentifier(hashAlgorithm)
    End Function

    Public Overrides Function SignData(data As Byte(), hashAlgorithm As HashAlgorithmName) As Byte()
        Return m_client.SignData(m_signingAlgorithm, data).Signature
    End Function

    Protected Shared Function GetHashAlgorithmName(signingAlgorithm As String) As HashAlgorithmName
        If signingAlgorithm.EndsWith("256") Then Return HashAlgorithmName.SHA256
        If signingAlgorithm.EndsWith("384") Then Return HashAlgorithmName.SHA384
        If signingAlgorithm.EndsWith("512") Then Return HashAlgorithmName.SHA512
        Throw New ArgumentException("Cannot determine hash algorithm for " & signingAlgorithm, NameOf(signingAlgorithm))
    End Function

    Protected Overrides Function BuildPublicKey() As PublicKey
        Return m_baseGenerator.PublicKey
    End Function
End Class
