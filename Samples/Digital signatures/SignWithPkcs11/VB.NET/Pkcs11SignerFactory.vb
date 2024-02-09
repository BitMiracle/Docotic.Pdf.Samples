Imports System.Security.Cryptography.X509Certificates
Imports BitMiracle.Docotic.Pdf
Imports Net.Pkcs11Interop.Common
Imports Net.Pkcs11Interop.HighLevelAPI
Imports Net.Pkcs11Interop.HighLevelAPI.Factories

Namespace BitMiracle.Docotic.Pdf.Samples
    Class Pkcs11SignerFactory
        Implements IDisposable

        Private ReadOnly m_pkcs11Library As IPkcs11Library
        Private ReadOnly m_slot As ISlot
        Private m_disposed As Boolean

        Public Sub New(libraryPath As String, slotId As ULong)
            Dim factories = New Pkcs11InteropFactories()
            m_pkcs11Library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, libraryPath, AppType.MultiThreaded)
            m_slot = m_pkcs11Library.GetSlotList(SlotsType.WithOrWithoutTokenPresent).First(Function(slot) slot.SlotId = slotId)
        End Sub

        Public Function Load([alias] As String, certLabel As String, pin As String, digestAlgorithm As PdfDigestAlgorithm) As Pkcs11Signer
            Dim pkAttributeKeys = New List(Of CKA)() From {
                CKA.CKA_KEY_TYPE,
                CKA.CKA_LABEL,
                CKA.CKA_ID
            }
            Dim certAttributeKeys = New List(Of CKA)() From {
                CKA.CKA_VALUE,
                CKA.CKA_LABEL
            }
            Dim disposeSession As Boolean = True
            Dim session As ISession = m_slot.OpenSession(SessionType.[ReadOnly])

            Try
                session.Login(CKU.CKU_USER, pin)
                Dim objectAttributeFactory = New ObjectAttributeFactory()
                Dim attributes = New List(Of IObjectAttribute) From {
                    objectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY)
                }
                Dim keys As List(Of IObjectHandle) = session.FindAllObjects(attributes)
                Dim signatureAlgorithm As PdfSignatureAlgorithm? = Nothing
                Dim chain = New List(Of X509Certificate2)()
                Dim privateKey As IObjectHandle = Nothing
                Dim found As Boolean = False

                For Each key As IObjectHandle In keys
                    Dim keyAttributes As List(Of IObjectAttribute) = session.GetAttributeValue(key, pkAttributeKeys)
                    Dim type As ULong = keyAttributes(0).GetValueAsUlong()

                    Select Case type
                        Case CULng(CKK.CKK_RSA)
                            signatureAlgorithm = PdfSignatureAlgorithm.Rsa
                        Case CULng(CKK.CKK_ECDSA)
                            signatureAlgorithm = PdfSignatureAlgorithm.Ecdsa
                        Case Else
                            Continue For
                    End Select

                    Dim thisAlias As String = keyAttributes(1).GetValueAsString()
                    If thisAlias Is Nothing OrElse thisAlias.Length = 0 Then thisAlias = keyAttributes(2).GetValueAsString()
                    If [alias] IsNot Nothing AndAlso Not [alias].Equals(thisAlias) Then Continue For
                    attributes.Clear()
                    attributes.Add(objectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE))
                    attributes.Add(objectAttributeFactory.Create(CKA.CKA_CERTIFICATE_TYPE, CKC.CKC_X_509))
                    If certLabel Is Nothing AndAlso thisAlias IsNot Nothing AndAlso thisAlias.Length > 0 Then certLabel = thisAlias
                    If certLabel IsNot Nothing Then attributes.Add(objectAttributeFactory.Create(CKA.CKA_LABEL, certLabel))
                    Dim certificates As List(Of IObjectHandle) = session.FindAllObjects(attributes)
                    If certificates.Count <> 1 Then Continue For
                    Dim certificate As IObjectHandle = certificates(0)
                    Dim certificateAttributes As List(Of IObjectAttribute) = session.GetAttributeValue(certificate, certAttributeKeys)
                    Dim cert = New X509Certificate2(certificateAttributes(0).GetValueAsByteArray())
                    chain = New List(Of X509Certificate2) From {
                        cert
                    }

                    Try
                        attributes.Clear()
                        attributes.Add(objectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE))
                        attributes.Add(objectAttributeFactory.Create(CKA.CKA_CERTIFICATE_TYPE, CKC.CKC_X_509))
                        Dim otherCertificates As List(Of IObjectHandle) = session.FindAllObjects(attributes)

                        For Each otherCertificate As IObjectHandle In otherCertificates

                            If Not certificate.ObjectId.Equals(otherCertificate.ObjectId) Then
                                certificateAttributes = session.GetAttributeValue(otherCertificate, certAttributeKeys)
                                Dim otherCert = New X509Certificate2(certificateAttributes(0).GetValueAsByteArray())
                                chain.Add(otherCert)
                            End If
                        Next

                    Catch

                        For Each c In chain
                            c.Dispose()
                        Next

                        Throw
                    End Try

                    found = True
                    privateKey = key
                    Exit For
                Next

                If Not found Then Return Nothing
                disposeSession = False
                Return New Pkcs11Signer(signatureAlgorithm.Value, digestAlgorithm, chain.ToArray(), session, privateKey)
            Finally
                If disposeSession Then session.Dispose()
            End Try
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            If m_disposed Then Return

            m_disposed = True

            m_pkcs11Library?.Dispose()

            GC.SuppressFinalize(Me)
        End Sub
    End Class
End Namespace
