Imports System.Diagnostics

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignSignatureFieldUsingCustomStyle
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim outputFileName As String = "SignSignatureFieldUsingCustomStyle.pdf"

            Using pdf As PdfDocument = New PdfDocument("Sample data/SignatureFields.pdf")
                ' IMPORTANT:
                ' Replace "keystore.p12" and "password" with your own .p12 or .pfx path and password.
                ' Without the change the sample will not work.

                Dim field As PdfSignatureField = TryCast(pdf.GetControl("Control"), PdfSignatureField)
                Dim options As PdfSigningOptions = New PdfSigningOptions("keystore.p12", "password") With {
                    .DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    .Format = PdfSignatureFormat.Pkcs7Detached,
                    .Field = field,
                    .Reason = "Testing field styles",
                    .Location = "My workplace",
                    .ContactInfo = "support@example.com"
                }
                options.Appearance.IncludeDate = False
                options.Appearance.IncludeDistinguishedName = False

                options.Appearance.NameLabel = "Digital signiert von"
                options.Appearance.ReasonLabel = "Grund:"
                options.Appearance.LocationLabel = "Ort:"

                pdf.SignAndSave(options, outputFileName)
            End Using

            Process.Start(outputFileName)
        End Sub
    End Class
End Namespace