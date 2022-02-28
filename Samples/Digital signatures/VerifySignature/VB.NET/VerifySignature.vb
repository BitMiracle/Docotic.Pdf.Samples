Imports System.Text

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class VerifySignature
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim sb As StringBuilder = New StringBuilder()
            Using pdf As PdfDocument = New PdfDocument("..\Sample Data\signed.pdf")
                Dim field As PdfControl = pdf.GetControls().FirstOrDefault(Function(c) c.Type = PdfWidgetType.Signature)
                If field Is Nothing Then
                    Console.WriteLine("Document does not contain signature fields", "Verification result")
                    Return
                End If

                Dim signature = CType(field, PdfSignatureField).Signature
                Dim contents = signature.Contents
                sb.AppendFormat("Signed part is intact: {0}" & vbLf, contents.VerifyDigest())

                Dim signingTime = If(signature.SigningTime, Date.MinValue)
                sb.AppendFormat("Signed on: {0}" & vbLf, signingTime.ToShortDateString())

                Dim timestampToken = contents.GetTimestampToken()
                If timestampToken IsNot Nothing Then
                    sb.AppendFormat("Embedded timestamp: {0}" & vbLf, timestampToken.GenerationTime)

                    If timestampToken.TimestampAuthority IsNot Nothing Then
                        sb.AppendFormat("Timestamp authority: {0}" & vbLf, timestampToken.TimestampAuthority.Name)
                    End If

                    sb.AppendFormat("Timestamp is intact: {0}" & vbLf & vbLf, contents.VerifyTimestamp())
                Else
                    sb.AppendLine()
                End If

                If contents.CheckHasEmbeddedOcsp() Then
                    sb.AppendLine("Signature has OCSP embedded.")
                    checkRevocation(signature, sb, PdfCertificateRevocationCheckMode.EmbeddedOcsp)
                End If

                If contents.CheckHasEmbeddedCrl() Then
                    sb.AppendLine("Signature has CRL embedded.")
                    checkRevocation(signature, sb, PdfCertificateRevocationCheckMode.EmbeddedCrl)
                End If

                checkRevocation(signature, sb, PdfCertificateRevocationCheckMode.OnlineOcsp)
                checkRevocation(signature, sb, PdfCertificateRevocationCheckMode.OnlineCrl)
            End Using

            Console.WriteLine(sb.ToString(), "Verification result")
        End Sub

        Private Shared Sub checkRevocation(ByVal signature As PdfSignature, ByVal sb As StringBuilder, ByVal mode As PdfCertificateRevocationCheckMode)
            Dim contents = signature.Contents
            Dim signingTime = If(signature.SigningTime, Date.MinValue)

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