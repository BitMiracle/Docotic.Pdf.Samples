Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignDocumentEmbedTimestamp
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim outputFileName = "SignDocumentEmbedTimestamp.pdf"
            Using pdf As New PdfDocument("..\Sample Data\jpeg.pdf")
                ' IMPORTANT:
                ' Replace "keystore.p12" And "password" with your own .p12 Or .pfx path And password.
                ' Without the change the sample will Not work.

                Dim options As New PdfSigningOptions("keystore.p12", "password") With {
                    .DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    .Format = PdfSignatureFormat.CadesDetached,
                    .Reason = "Testing timestamp embedding",
                    .Location = "My workplace",
                    .ContactInfo = "support@example.com"
                }

                ' Replace the following test URL with your Timestamp Authority URL
                options.Timestamp.AuthorityUrl = New Uri("http://timestamp.digicert.com")

                ' Specify username And password if your Timestamp Authority requires authentication
                options.Timestamp.Username = Nothing
                options.Timestamp.Password = Nothing

                pdf.SignAndSave(options, outputFileName)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(outputFileName) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace