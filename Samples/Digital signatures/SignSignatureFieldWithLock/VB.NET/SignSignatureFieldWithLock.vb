Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class SignSignatureFieldWithLock
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "SignSignatureFieldWithLock.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                Dim first = page.AddSignatureField("first", 100, 100, 200, 50)
                ' when the first field is signed, fields with "some_text_field" and
                ' "some_checkbox" names will be locked for editing
                first.Lock = PdfSignatureFieldLock.CreateLockFields("some_text_field", "some_checkbox")

                Dim second = page.AddSignatureField("second", 100, 200, 200, 50)
                ' when the second field is signed, all fields will be locked for editing
                second.Lock = PdfSignatureFieldLock.CreateLockAll()

                ' IMPORTANT
                ' Replace "keystore.p12" And "password" with your own .p12 or .pfx path and password.
                ' Without the change the sample will not work.
                Dim options As New PdfSigningOptions("keystore.p12", "password") With {
                    .DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    .Format = PdfSignatureFormat.Pkcs7Detached,
                    .Field = first,
                    .Type = PdfSignatureType.AuthorFormFillingAllowed,
                    .Reason = "Testing field locking",
                    .Location = "My workplace",
                    .ContactInfo = "support@example.com"
                }
                pdf.SignAndSave(options, pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace