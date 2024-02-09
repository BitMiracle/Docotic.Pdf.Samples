Imports System.Security.Cryptography.X509Certificates
Imports BitMiracle.Docotic.Pdf
Imports Net.Pkcs11Interop.Common
Imports Net.Pkcs11Interop.HighLevelAPI
Imports Net.Pkcs11Interop.HighLevelAPI.Factories

Namespace BitMiracle.Docotic.Pdf.Samples
    Class Pkcs11Signer
        Implements IPdfSigner
        Implements IDisposable

        Private ReadOnly m_session As ISession
        Private ReadOnly m_privateKey As IObjectHandle
        Private ReadOnly m_signatureAlgorithm As PdfSignatureAlgorithm
        Private ReadOnly m_digestAlgorithm As PdfDigestAlgorithm
        Private ReadOnly m_chain As X509Certificate2()
        Private m_disposed As Boolean

        Public Sub New(
            signatureAlgorithm As PdfSignatureAlgorithm,
            digestAlgorithm As PdfDigestAlgorithm,
            chain As X509Certificate2(),
            session As ISession,
            privateKey As IObjectHandle)

            m_signatureAlgorithm = signatureAlgorithm
            m_digestAlgorithm = digestAlgorithm
            m_chain = chain
            m_session = session
            m_privateKey = privateKey
        End Sub

        Public ReadOnly Property SignatureAlgorithm As PdfSignatureAlgorithm Implements IPdfSigner.SignatureAlgorithm
            Get
                Return m_signatureAlgorithm
            End Get
        End Property

        Public Function Sign(message As Byte()) As Byte() Implements IPdfSigner.Sign
            Dim mechanismFactory = New MechanismFactory()
            Dim mechanism As IMechanism
            If m_signatureAlgorithm = PdfSignatureAlgorithm.Ecdsa Then

                If m_digestAlgorithm = PdfDigestAlgorithm.Sha1 Then
                    mechanism = mechanismFactory.Create(CKM.CKM_ECDSA_SHA1)
                ElseIf m_digestAlgorithm = PdfDigestAlgorithm.Sha256 Then
                    mechanism = mechanismFactory.Create(CKM.CKM_ECDSA_SHA256)
                ElseIf m_digestAlgorithm = PdfDigestAlgorithm.Sha384 Then
                    mechanism = mechanismFactory.Create(CKM.CKM_ECDSA_SHA384)
                ElseIf m_digestAlgorithm = PdfDigestAlgorithm.Sha512 Then
                    mechanism = mechanismFactory.Create(CKM.CKM_ECDSA_SHA512)
                Else
                    Throw New InvalidOperationException("Unexpected digest algorithm " & m_digestAlgorithm)
                End If
            ElseIf m_signatureAlgorithm = PdfSignatureAlgorithm.Rsa Then

                If m_digestAlgorithm = PdfDigestAlgorithm.Sha1 Then
                    mechanism = mechanismFactory.Create(CKM.CKM_SHA1_RSA_PKCS)
                ElseIf m_digestAlgorithm = PdfDigestAlgorithm.Sha256 Then
                    mechanism = mechanismFactory.Create(CKM.CKM_SHA256_RSA_PKCS)
                ElseIf m_digestAlgorithm = PdfDigestAlgorithm.Sha384 Then
                    mechanism = mechanismFactory.Create(CKM.CKM_SHA384_RSA_PKCS)
                ElseIf m_digestAlgorithm = PdfDigestAlgorithm.Sha512 Then
                    mechanism = mechanismFactory.Create(CKM.CKM_SHA512_RSA_PKCS)
                Else
                    Throw New InvalidOperationException("Unexpected digest algorithm " & m_digestAlgorithm)
                End If
            Else
                Throw New InvalidOperationException("Unexpected signature algorithm " & m_signatureAlgorithm)
            End If

            Return m_session.Sign(mechanism, m_privateKey, message)
        End Function

        Public Function GetChain() As X509Certificate2()
            Return m_chain
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            If m_disposed Then Return
            m_disposed = True
            m_session.Dispose()

            For Each c In m_chain
                c.Dispose()
            Next

            GC.SuppressFinalize(Me)
        End Sub
    End Class
End Namespace