Imports System.Text

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CheckIfPasswordProtected
        Public Shared Sub Main()
            Dim message As New StringBuilder()

            Dim documentsToCheck As String() = {"encrypted.pdf", "jfif3.pdf", "public-key-encrypted.pdf"}
            For Each fileName As String In documentsToCheck
                Dim info As PdfEncryptionInfo = PdfDocument.GetEncryptionInfo("..\Sample Data\" & fileName)
                If info Is Nothing Then
                    message.AppendFormat("{0} - is not encrypted" & vbCrLf, fileName)
                ElseIf TypeOf info Is PdfStandardEncryptionInfo Then
                    Dim standardInfo As PdfStandardEncryptionInfo = info
                    If standardInfo.RequiresPasswordToOpen Then
                        message.AppendFormat("{0} - is encrypted and requires password" & vbCrLf, fileName)
                    Else
                        message.AppendFormat("{0} - is encrypted and doesn't require password" & vbCrLf, fileName)
                    End If
                ElseIf TypeOf info Is PdfPublicKeyEncryptionInfo Then
                    Dim publicKeyInfo As PdfPublicKeyEncryptionInfo = info
                    message.AppendFormat("{0} - is encrypted and requires one of the certificates: ", fileName)
                    Dim serialNumbers As String() = publicKeyInfo.Recipients.Select(Function(r) r.SerialNumber).ToArray()
                    message.Append(String.Join(", ", serialNumbers))
                End If
            Next

            Console.WriteLine(message.ToString())
        End Sub
    End Class
End Namespace