Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignByMultiplePeople
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            ' IMPORTANT:
            ' Please replace keystore paths and passwords in the following declarations with
            ' your own .p12 or .pfx path and password. Without the changes, the sample will not work.
            ' For testing, you can use the same keystore for all signers.

            Dim signers = New Credentials() {
                New Credentials("Frida Palacios", "keystore.p12", "password"),
                New Credentials("Dax Yu", "keystore.p12", "password"),
                New Credentials("Lana Conway", "keystore.p12", "password"),
                New Credentials("Paulina Mann", "keystore.p12", "password")
            }

            ' Start by filling in the requester name. Then sign as an author, disallowing
            ' all further changes to the document except the filling and signing.
            Dim SignByMultiplePeopleStep1 = "SignByMultiplePeopleStep1.pdf"
            Using sourcePdf = New PdfDocument("..\Sample Data\shipping-request.pdf")
                SetText(sourcePdf, "goods", "A dog, a cat, and a small hat. That's it.")
                SetText(sourcePdf, "requestedByName", signers(0).Name)

                SignAndSave(
                    sourcePdf,
                    SignByMultiplePeopleStep1,
                    "requestedBySignature",
                    signers(0),
                    PdfSignatureType.AuthorFormFillingAllowed,
                    "I created this request",
                    "A nice spot",
                    "email@example.com")
            End Using

            ' Open the document signed by the requester. Then sign as the registerer of the request.
            Dim SignByMultiplePeopleStep2 = "SignByMultiplePeopleStep2.pdf"
            Using sourcePdf = New PdfDocument(SignByMultiplePeopleStep1)
                SetText(sourcePdf, "registeredByName", signers(1).Name)

                SignAndSave(
                    sourcePdf,
                    SignByMultiplePeopleStep2,
                    "registeredBySignature",
                    signers(1),
                    PdfSignatureType.Approval,
                    "I registered this request",
                    "My worklace",
                    "email@example2.com")
            End Using

            ' Open the document signed by the reqisterer. Then sign as the person who approved the request.
            Dim SignByMultiplePeopleStep3 = "SignByMultiplePeopleStep3.pdf"
            Using sourcePdf = New PdfDocument(SignByMultiplePeopleStep2)
                SetText(sourcePdf, "approvedByName", signers(2).Name)

                SignAndSave(
                    sourcePdf,
                    SignByMultiplePeopleStep3,
                    "approvedBySignature",
                    signers(2),
                    PdfSignatureType.Approval,
                    "I approved this request",
                    "My worklace",
                    "approvals@example2.com")
            End Using

            ' Open the approved and signed document. Then sign as the person who executed the request.
            Dim SignByMultiplePeopleStep4 = "SignByMultiplePeopleStep4.pdf"
            Using sourcePdf = New PdfDocument(SignByMultiplePeopleStep3)
                SetText(sourcePdf, "executedByName", signers(3).Name)

                SignAndSave(
                    sourcePdf,
                    SignByMultiplePeopleStep4,
                    "executedBySignature",
                    signers(3),
                    PdfSignatureType.Approval,
                    "I approved this request",
                    "My worklace",
                    "office@example2.com")
            End Using

            ' Uncomment if you would like to open previous steps results, too.
            'OpenResult(SignByMultiplePeopleStep1)
            'OpenResult(SignByMultiplePeopleStep2)
            'OpenResult(SignByMultiplePeopleStep3)
            OpenResult(SignByMultiplePeopleStep4)

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub

        Private Shared Sub SignAndSave(
            source As PdfDocument,
            destFileName As String,
            signatureFieldName As String,
            credentials As Credentials,
            signatureType As PdfSignatureType,
            reason As String,
            location As String,
            contactInfo As String
        )
            Dim field As PdfSignatureField = GetControl(Of PdfSignatureField)(source, signatureFieldName)
            Dim signingOptions = New PdfSigningOptions(credentials.Keystore, credentials.Password) With {
                .DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                .Format = PdfSignatureFormat.Pkcs7Detached,
                .Field = field,
                .Type = signatureType,
                .Reason = reason,
                .Location = location,
                .ContactInfo = contactInfo
            }

            ' It is extremely important to write the resulting file incrementally.
            ' Otherwise, the signature in the source file will be invalidated.
            Dim saveOptions = New PdfSaveOptions With {
                .WriteIncrementally = True,
                .UseObjectStreams = False
            }

            source.SignAndSave(signingOptions, destFileName, saveOptions)
        End Sub

        Private Shared Sub SetText(pdf As PdfDocument, textBoxName As String, text As String)
            Dim textBox = GetControl(Of PdfTextBox)(pdf, textBoxName)
            If textBox Is Nothing Then
                Throw New ArgumentException($"Unable to find a text box by name = '{textBoxName}'", NameOf(textBoxName))
            End If

            textBox.Text = text
        End Sub

        Private Shared Sub OpenResult(path As String)
            Process.Start(New ProcessStartInfo(path) With {
                .UseShellExecute = True
            })
        End Sub

        Private Shared Function GetControl(Of T As Class)(pdf As PdfDocument, name As String) As T
            Dim control As PdfControl = Nothing
            If Not pdf.TryGetControl(name, control) Then
                Return Nothing
            End If

            Return TryCast(control, T)
        End Function
    End Class
End Namespace