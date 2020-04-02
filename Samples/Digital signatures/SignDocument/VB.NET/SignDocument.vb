Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignDocument
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim outputFileName = "SignDocument.pdf"

            Using pdf As PdfDocument = New PdfDocument("Sample data/jpeg.pdf")
                ' NOTE:
                ' You MUST replace the path and the password parameters passed to
                ' the PdfSigningOptions constructor with your own .p12 or .pfx path and password.
                ' Otherwise the sample will not work.
                '
                Dim options As PdfSigningOptions = New PdfSigningOptions("keystore.p12", "password") With {
                    .DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    .Format = PdfSignatureFormat.Pkcs7Detached,
                    .Reason = "Testing digital signatures",
                    .Location = "My workplace",
                    .ContactInfo = "support@example.com"
                }
                pdf.SignAndSave(options, outputFileName)
            End Using

            Process.Start(outputFileName)
        End Sub
    End Class
End Namespace