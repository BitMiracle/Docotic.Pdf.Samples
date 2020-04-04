Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class VerifySignature
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim sb As StringBuilder = New StringBuilder()

            Using pdf As PdfDocument = New PdfDocument("Sample data/signed.pdf")
                Dim control As PdfControl = pdf.GetControls().FirstOrDefault(Function(x) TypeOf x Is PdfSignatureField)
                Dim field = CType(control, PdfSignatureField)
                Dim contents = field.Signature.Contents
                sb.AppendFormat("Signed part is intact: {0}" & vbLf, contents.VerifyDigest())

                Dim signingTime = If(field.Signature.SigningTime, Date.MinValue)
                sb.AppendFormat("Signed on: {0}" & vbLf & vbLf, signingTime.ToShortDateString())

                If contents.CheckHasEmbeddedOcsp() Then
                    sb.AppendLine("Signature has OCSP embedded.")
                    checkRevocation(field, sb, PdfCertificateRevocationCheckMode.EmbeddedOcsp)
                End If

                If contents.CheckHasEmbeddedCrl() Then
                    sb.AppendLine("Signature has CRL embedded.")
                    checkRevocation(field, sb, PdfCertificateRevocationCheckMode.EmbeddedCrl)
                End If

                checkRevocation(field, sb, PdfCertificateRevocationCheckMode.OnlineOcsp)
                checkRevocation(field, sb, PdfCertificateRevocationCheckMode.OnlineCrl)
            End Using

            MessageBox.Show(sb.ToString(), "Verification result")
        End Sub

        Private Shared Sub checkRevocation(ByVal field As PdfSignatureField, ByVal sb As StringBuilder, ByVal mode As PdfCertificateRevocationCheckMode)
            Dim contents = field.Signature.Contents
            Dim signingTime = If(field.Signature.SigningTime, Date.MinValue)

            For Each time In {signingTime, Date.UtcNow}
                Dim revoked = contents.CheckIfRevoked(mode, time)
                Dim status = If(revoked, "Revoked", "Valid")
                Dim [date] As String = time.ToShortDateString()
                sb.AppendFormat("Checking using {0} mode: {1} on {2}" & vbLf, mode, status, [date])
            Next

            sb.AppendLine()
        End Sub
    End Class
End Namespace