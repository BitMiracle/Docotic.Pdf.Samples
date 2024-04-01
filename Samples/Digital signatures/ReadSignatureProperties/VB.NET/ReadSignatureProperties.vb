Imports System.Security.Cryptography.X509Certificates
Imports System.Text

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ReadSignatureProperties
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim sb As New StringBuilder()
            Using pdf As New PdfDocument("..\Sample Data\signed.pdf")
                Dim control As PdfControl = pdf.GetControls().FirstOrDefault(Function(c) c.Type = PdfWidgetType.Signature)
                If control Is Nothing Then
                    Console.WriteLine("Document does not contain signature fields")
                    Return
                End If

                Dim field = CType(control, PdfSignatureField)
                sb.AppendFormat("Signature field is invisible: {0}" & vbLf, IsInvisible(field))

                Dim signature = field.Signature
                If signature Is Nothing Then
                    sb.AppendLine("Signature field does not have an associated signature")
                    Console.WriteLine(sb.ToString())
                    Return
                End If

                sb.AppendFormat("Signed by: {0}" & vbLf, signature.Name)
                sb.AppendFormat("Signing time: {0}" & vbLf, signature.SigningTime)
                sb.AppendFormat("Signed at: {0}" & vbLf, signature.Location)
                sb.AppendFormat("Reason for signing: {0}" & vbLf, signature.Reason)
                sb.AppendFormat("Signer's contact: {0}" & vbLf, signature.ContactInfo)

                Dim contents = signature.Contents
                sb.AppendFormat("Has OCSP embedded: {0}" & vbLf, contents.CheckHasEmbeddedOcsp())
                sb.AppendFormat("Has CRL embedded: {0}" & vbLf, contents.CheckHasEmbeddedCrl())

                Dim certificate As PdfSignatureCertificate = contents.GetSigningCertificate()
                sb.AppendLine()
                sb.AppendLine("== Signing certificate:")
                sb.AppendFormat("Name: {0}" & vbLf, certificate.Name)
                sb.AppendFormat("Algorithm: {0}" & vbLf, certificate.AlgorithmName)
                sb.AppendFormat("Subject DN: {0}" & vbLf, certificate.Subject.Name)
                sb.AppendFormat("Issuer DN: {0}" & vbLf, certificate.Issuer.Name)
                sb.AppendFormat("Serial number: {0}" & vbLf, certificate.SerialNumber)
                sb.AppendFormat("Valid from {0} up to {1}" & vbLf, certificate.ValidFrom, certificate.ValidUpto)
                sb.AppendFormat("Timestamp Authority URL: {0}" & vbLf, certificate.GetTimestampAuthorityUrl())

                sb.AppendLine()
                sb.AppendLine("== Issuer certificate:")

                Dim issuer = contents.GetIssuerCertificateFor(certificate)
                If issuer Is Nothing Then
                    sb.AppendLine("Not embedded in the PDF: true")

                    Dim issuer2 As X509Certificate2 = FindCertificateByIssuerName(certificate.Issuer)
                    If issuer2 IsNot Nothing Then
                        sb.AppendLine("Found in a local list of certificates: true")
                        sb.AppendFormat("Subject DN: {0}" & vbLf, issuer2.Subject)
                        sb.AppendFormat("Issuer DN: {0}" & vbLf, issuer2.Issuer)
                        sb.AppendFormat("Serial number: {0}" & vbLf, issuer2.SerialNumber)
                    End If
                Else
                    sb.AppendFormat("Subject DN: {0}" & vbLf, issuer.Subject.Name)
                    sb.AppendFormat("Issuer DN: {0}" & vbLf, issuer.Issuer.Name)
                    sb.AppendFormat("Serial number: {0}" & vbLf, issuer.SerialNumber)
                End If
            End Using

            Console.WriteLine(sb.ToString())
        End Sub

        Private Shared Function IsInvisible(field As PdfSignatureField) As Boolean
            Return field.Width = 0 AndAlso field.Height = 0 OrElse field.Flags.HasFlag(PdfWidgetFlags.Hidden) OrElse field.Flags.HasFlag(PdfWidgetFlags.NoView)
        End Function

        Private Shared Function FindCertificateByIssuerName(issuerName As X500DistinguishedName) As X509Certificate2
            Using certificatesStore = New X509Store(StoreName.CertificateAuthority, StoreLocation.CurrentUser)
                certificatesStore.Open(OpenFlags.OpenExistingOnly)

                Dim searchType = X509FindType.FindBySubjectDistinguishedName
                Dim findValue As String = issuerName.Name
                Dim matchingCertificates As X509Certificate2Collection = certificatesStore.Certificates.Find(searchType,
                    findValue, False)

                Return If(matchingCertificates.Count > 0, matchingCertificates(0), Nothing)
            End Using
        End Function
    End Class
End Namespace