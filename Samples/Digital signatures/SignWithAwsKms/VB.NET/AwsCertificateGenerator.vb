Imports Amazon.KeyManagementService
Imports Amazon.KeyManagementService.Model
Imports System.IO
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates

Class AwsCertificateGenerator
    Inherits X509SignatureGenerator

    Private Const SubjectDn As String = "CN=self-generated cert,OU=Docotic.Pdf samples,O=Bit Miracle"
    Private Shared ReadOnly SerialNumber As Byte() = New Byte() {17, 59, 46}
    Private Shared ReadOnly NotBefore As DateTimeOffset = Date.UtcNow
    Private Shared ReadOnly NotAfter As DateTimeOffset = NotBefore.AddYears(2)

    Private ReadOnly m_baseGenerator As X509SignatureGenerator
    Private ReadOnly m_keyId As String
    Private ReadOnly m_signingAlgorithm As SigningAlgorithmSpec

    Private Sub New(keyId As String, alg As SigningAlgorithmSpec, baseGenerator As X509SignatureGenerator)
        m_baseGenerator = baseGenerator
        m_keyId = keyId
        m_signingAlgorithm = alg
    End Sub

    Public Shared Function Generate(key As GetPublicKeyResponse, signingAlgorithm As SigningAlgorithmSpec) As X509Certificate2
        Return Generate(key, signingAlgorithm, SubjectDn, NotBefore, NotAfter, SerialNumber)
    End Function

    Public Shared Function Generate(
        key As GetPublicKeyResponse,
        signingAlgorithm As SigningAlgorithmSpec,
        subjectDN As String,
        notBefore As DateTimeOffset,
        notAfter As DateTimeOffset,
        serialNumber As Byte()) As X509Certificate2

        Dim pkBytes As Byte() = key.PublicKey.ToArray()
        Dim hashAlg As HashAlgorithmName = GetHashAlgorithmName(signingAlgorithm)
        Dim keySpecString As String = key.KeySpec.ToString()
        Dim certificateRequest As CertificateRequest
        Dim baseGenerator As X509SignatureGenerator

        Dim tmp As Integer
        If keySpecString.StartsWith("ECC") Then
            Dim ecdsa As ECDsa = ECDsa.Create()
            ecdsa.ImportSubjectPublicKeyInfo(pkBytes, tmp)

            certificateRequest = New CertificateRequest(subjectDN, ecdsa, hashAlg)
            baseGenerator = CreateForECDsa(ecdsa)
        ElseIf keySpecString.StartsWith("RSA") Then
            Dim rsa As RSA = RSA.Create()
            rsa.ImportSubjectPublicKeyInfo(pkBytes, tmp)

            Dim alg As String = signingAlgorithm.ToString()
            Dim padding As RSASignaturePadding = If(alg.StartsWith("RSASSA_PKCS1_V1_5"), RSASignaturePadding.Pkcs1, RSASignaturePadding.Pss)
            certificateRequest = New CertificateRequest(subjectDN, rsa, hashAlg, padding)
            baseGenerator = CreateForRSA(rsa, padding)
        Else
            Throw New ArgumentException("Cannot determine encryption algorithm for " & keySpecString, NameOf(key))
        End If

        Dim generator = New AwsCertificateGenerator(key.KeyId, signingAlgorithm, baseGenerator)
        Return certificateRequest.Create(New X500DistinguishedName(subjectDN), generator, notBefore, notAfter, serialNumber)
    End Function

    Public Overrides Function GetSignatureAlgorithmIdentifier(hashAlgorithm As HashAlgorithmName) As Byte()
        Return m_baseGenerator.GetSignatureAlgorithmIdentifier(hashAlgorithm)
    End Function

    Public Overrides Function SignData(data As Byte(), hashAlgorithm As HashAlgorithmName) As Byte()
        Using kmsClient = New AmazonKeyManagementServiceClient()
            Using stream = New MemoryStream(data)
                Dim signRequest = New SignRequest() With {
                .SigningAlgorithm = m_signingAlgorithm.ToString(),
                .KeyId = m_keyId,
                .MessageType = MessageType.RAW,
                .Message = stream
            }
                Dim signResponse As SignResponse = kmsClient.SignAsync(signRequest).GetAwaiter().GetResult()
                Return signResponse.Signature.ToArray()
            End Using
        End Using
    End Function

    Protected Overrides Function BuildPublicKey() As PublicKey
        Return m_baseGenerator.PublicKey
    End Function

    Private Shared Function GetHashAlgorithmName(signingAlgorithm As String) As HashAlgorithmName
        If signingAlgorithm.EndsWith("256") Then Return HashAlgorithmName.SHA256
        If signingAlgorithm.EndsWith("384") Then Return HashAlgorithmName.SHA384
        If signingAlgorithm.EndsWith("512") Then Return HashAlgorithmName.SHA512
        Throw New ArgumentException("Cannot determine hash algorithm for " & signingAlgorithm, NameOf(signingAlgorithm))
    End Function
End Class
