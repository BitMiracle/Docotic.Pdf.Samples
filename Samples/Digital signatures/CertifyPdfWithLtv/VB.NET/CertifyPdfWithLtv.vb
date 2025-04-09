Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class CertifyPdfWithLtv
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim outputFileName = "CertifyPdfWithLtv.pdf"
            Using pdf As New PdfDocument()
                ' IMPORTANT:
                ' Replace "keystore.p12" And "password" with your own .p12 or .pfx path and password.
                ' Without the change, the sample will not work.

                Dim options As New PdfSigningOptions("keystore.p12", "password") With {
                    .DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    .Format = PdfSignatureFormat.CadesDetached,
                    .Type = PdfSignatureType.AuthorNoChanges
                }
                options.Timestamp.AuthorityUrl = New Uri("http://timestamp.digicert.com")

                pdf.AddLtvInfo(options)

                pdf.SignAndSave(options, outputFileName)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(outputFileName) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace