Imports System
Imports System.Linq
Imports System.Text
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ReadSignatureProperties
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim sb As StringBuilder = New StringBuilder()

            Using pdf As PdfDocument = New PdfDocument("Sample data/signed.pdf")
                Dim control As PdfControl = pdf.GetControls().FirstOrDefault(Function(c) c.Type = PdfWidgetType.Signature)
                If control Is Nothing Then
                    Console.WriteLine("Document does not contain signature fields")
                    Return
                End If

                Dim field = CType(control, PdfSignatureField)
                sb.AppendFormat("Signature field is invisible: {0}" & vbLf, isInvisible(field))

                Dim signature = field.Signature
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

                Dim issuer = contents.GetIssuerCertificateFor(certificate)
                sb.AppendLine()
                sb.AppendLine("== Issuer certificate:")
                sb.AppendFormat("Subject DN: {0}" & vbLf, issuer.Subject.Name)
                sb.AppendFormat("Issuer DN: {0}" & vbLf, issuer.Issuer.Name)
                sb.AppendFormat("Serial number: {0}" & vbLf, issuer.SerialNumber)
            End Using

            Console.WriteLine(sb.ToString(), "Signature Info")
        End Sub

        Private Shared Function isInvisible(ByVal field As PdfSignatureField) As Boolean
            Return field.Width = 0 AndAlso field.Height = 0 OrElse field.Flags.HasFlag(PdfWidgetFlags.Hidden) OrElse field.Flags.HasFlag(PdfWidgetFlags.NoView)
        End Function
    End Class
End Namespace