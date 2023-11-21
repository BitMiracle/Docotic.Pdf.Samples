Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignDocument
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim outputFileName = "SignDocument.pdf"
            Using pdf As New PdfDocument("..\Sample Data\jpeg.pdf")
                ' IMPORTANT:
                ' Replace "keystore.p12" And "password" with your own .p12 Or .pfx path And password.
                ' Without the change the sample will Not work.

                Dim options As New PdfSigningOptions("keystore.p12", "password") With {
                    .DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    .Format = PdfSignatureFormat.Pkcs7Detached,
                    .Reason = "Testing digital signatures",
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