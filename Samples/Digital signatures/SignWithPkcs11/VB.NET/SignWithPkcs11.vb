Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class SignWithAzureKeyVault
        Public Shared Sub Main()
            ' IMPORTANT
            ' Change these variables using your PCKS#11 driver, key info and digest algorithm.
            Dim pkcsLibraryPath As String = "C:\Program Files\SoftHSM2\lib\softhsm2-x64.dll"
            Dim slotId As ULong = 1743347971
            Dim keyAlias As String = "YOUR-ALIAS"
            Dim certLabel As String = "YOUR-CERTIFICATE"
            Dim pin As String = "YOUR-PIN"
            Dim digestAlgorithm = PdfDigestAlgorithm.Sha512

            Using factory = New Pkcs11SignerFactory(pkcsLibraryPath, slotId)
                Using signer As Pkcs11Signer = factory.Load(keyAlias, certLabel, pin, digestAlgorithm)
                    If signer Is Nothing Then
                        Console.WriteLine("Unable to load key. Make sure that the alias and the certificate label are correct.")
                        Return
                    End If

                    ' NOTE
                    ' When used in trial mode, the library imposes some restrictions.
                    ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions
                    ' for more information.
                    Dim outputFileName As String = "SignWithPkcs11.pdf"
                    Using pdf = New PdfDocument()
                        Dim page As PdfPage = pdf.Pages(0)
                        Dim field As PdfSignatureField = page.AddSignatureField(50, 50, 200, 200)

                        Dim options = New PdfSigningOptions(signer, signer.GetChain()) With {
                            .DigestAlgorithm = digestAlgorithm,
                            .Field = field
                        }
                        pdf.SignAndSave(options, outputFileName)
                    End Using

                    Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
                    Process.Start(New ProcessStartInfo(outputFileName) With {
                        .UseShellExecute = True
                    })
                End Using
            End Using
        End Sub
    End Class
End Namespace