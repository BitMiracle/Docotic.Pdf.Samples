Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignSignatureField
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim outputFileName = "SignSignatureField.pdf"

            Using pdf As PdfDocument = New PdfDocument("Sample data/SignatureFields.pdf")
                ' IMPORTANT:
                ' Replace "keystore.p12" And "password" with your own .p12 Or .pfx path And password.
                ' Without the change the sample will Not work.

                Dim field As PdfSignatureField = TryCast(pdf.GetControl("Signature2"), PdfSignatureField)
                Dim options As PdfSigningOptions = New PdfSigningOptions("keystore.p12", "password") With {
                    .DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    .Format = PdfSignatureFormat.Pkcs7Detached,
                    .Field = field,
                    .Reason = "Testing field signing",
                    .Location = "My workplace",
                    .ContactInfo = "support@example.com"
                }
                pdf.SignAndSave(options, outputFileName)
            End Using

            Process.Start(outputFileName)
        End Sub
    End Class
End Namespace