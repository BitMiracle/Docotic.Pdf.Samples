Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignAlreadySignedDocument
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            ' IMPORTANT
            ' Please replace
            ' * "your-signed-document.pdf" with the path to your file,
            ' * "keystore.p12" and "password" with your own .p12 or .pfx path and password.
            ' Without the changes, the sample will not work.

            Dim outputFileName = "SignAlreadySignedDocument.pdf"
            Using pdf As New PdfDocument("your-signed-document.pdf")
                Dim signingOptions As New PdfSigningOptions("keystore.p12", "password") With {
                    .DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    .Format = PdfSignatureFormat.Pkcs7Detached,
                    .Reason = "Adding signature to a signed document",
                    .Location = "My workplace",
                    .ContactInfo = "support@example.com"
                }

                Dim saveOptions As New PdfSaveOptions With {
                    .WriteIncrementally = True
                }

                pdf.SignAndSave(signingOptions, outputFileName, saveOptions)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(outputFileName) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace