Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignSignatureField
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim outputFileName = "SignSignatureField.pdf"
            Using pdf As New PdfDocument("..\Sample Data\SignatureFields.pdf")
                ' IMPORTANT:
                ' Replace "keystore.p12" And "password" with your own .p12 Or .pfx path And password.
                ' Without the change the sample will Not work.

                Dim field As PdfSignatureField = TryCast(pdf.GetControl("Signature2"), PdfSignatureField)
                If field Is Nothing Then
                    Console.WriteLine("Cannot find a signature field")
                    Return
                End If

                Dim options As New PdfSigningOptions("keystore.p12", "password") With {
                    .DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    .Format = PdfSignatureFormat.Pkcs7Detached,
                    .Field = field,
                    .Reason = "Testing field signing",
                    .Location = "My workplace",
                    .ContactInfo = "support@example.com"
                }
                pdf.SignAndSave(options, outputFileName)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(outputFileName) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace