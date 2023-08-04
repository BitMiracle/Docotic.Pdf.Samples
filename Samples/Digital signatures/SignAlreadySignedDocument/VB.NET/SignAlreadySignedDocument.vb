Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignDocument
        Public Shared Sub Main()
            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            ' This example won't work without a license. Please get a temporary license and
            ' apply it in the next line.
            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            ' IMPORTANT
            ' Please replace
            ' * "your-signed-document.pdf" with the path to your file,
            ' * "keystore.p12" And "password" with your own .p12 Or .pfx path And password.
            ' Without the changes the sample will not work.

            Dim outputFileName = "SignAlreadySignedDocument.pdf"
            Using pdf As PdfDocument = New PdfDocument("your-signed-document.pdf")
                Dim signingOptions As PdfSigningOptions = New PdfSigningOptions("keystore.p12", "password") With {
                    .DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    .Format = PdfSignatureFormat.Pkcs7Detached,
                    .Reason = "Adding signature to a signed document",
                    .Location = "My workplace",
                    .ContactInfo = "support@example.com"
                }

                Dim saveOptions As PdfSaveOptions = New PdfSaveOptions With {
                    .WriteIncrementally = True
                }

                pdf.SignAndSave(signingOptions, outputFileName, saveOptions)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(outputFileName) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace