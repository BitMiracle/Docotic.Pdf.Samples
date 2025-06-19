Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignSignatureField
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim outputFileName = "SignSignatureField.pdf"
            Using pdf As New PdfDocument("..\Sample Data\SignatureFields.pdf")
                ' IMPORTANT:
                ' Replace "keystore.p12" and "password" with your own .p12 or .pfx path and password.
                ' Without the change, the sample will not work.
                Dim field As PdfSignatureField = GetSignatureField(pdf, "Signature2")
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

        Private Shared Function GetSignatureField(pdf As PdfDocument, name As String) As PdfSignatureField
            Dim control As PdfControl = Nothing
            If Not pdf.TryGetControl(name, control) Then
                Return Nothing
            End If

            Return TryCast(control, PdfSignatureField)
        End Function
    End Class
End Namespace