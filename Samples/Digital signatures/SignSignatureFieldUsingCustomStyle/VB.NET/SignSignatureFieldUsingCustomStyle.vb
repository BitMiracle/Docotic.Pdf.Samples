Imports BitMiracle.Docotic

Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignSignatureFieldUsingCustomStyle
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim outputFileName As String = "SignSignatureFieldUsingCustomStyle.pdf"
            Using pdf As New PdfDocument("..\Sample Data\SignatureFields.pdf")
                ' IMPORTANT:
                ' Replace "keystore.p12" and "password" with your own .p12 or .pfx path and password.
                ' Without the change, the sample will not work.

                Dim field As PdfSignatureField = TryCast(pdf.GetControl("Control"), PdfSignatureField)
                If field Is Nothing Then
                    Console.WriteLine("Cannot find a signature field")
                    Return
                End If

                field.BackgroundColor = New PdfGrayColor(80)

                Dim options As New PdfSigningOptions("keystore.p12", "password") With {
                    .DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    .Format = PdfSignatureFormat.Pkcs7Detached,
                    .Field = field,
                    .Reason = "Testing field styles",
                    .Location = "My workplace",
                    .ContactInfo = "support@example.com"
                }

                Dim appearance = options.Appearance
                appearance.IncludeDate = False
                appearance.IncludeDistinguishedName = False

                appearance.Image = pdf.AddImage("..\Sample Data\ammerland.jpg")
                appearance.Font = pdf.AddFont(PdfBuiltInFont.Courier)
                appearance.FontSize = 0 ' calculate font size automatically
                appearance.FontColor = New PdfRgbColor(0, 0, 255)
                appearance.TextAlignment = PdfSignatureTextAlignment.Right

                appearance.NameLabel = "Digital signiert von"
                appearance.ReasonLabel = "Grund:"
                appearance.LocationLabel = "Ort:"

                pdf.SignAndSave(options, outputFileName)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(outputFileName) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace