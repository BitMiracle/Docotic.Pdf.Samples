Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class PdfDocumentBuilderSample
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Const PathToFile As String = "PdfDocumentBuilder.pdf"
            PdfDocumentBuilder.
                Create().
                Encryption(New PdfStandardEncryptionHandler("owner", "user")).
                Info(
                    Sub(info)
                        info.Author = "Sample author"
                        info.Title = "Sample title"
                    End Sub).
                Version(PdfVersion.Pdf16).Generate(PathToFile,
                    Sub(doc)
                        doc.Pages(Sub(p)
                                  End Sub)
                    End Sub)

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
            Process.Start(New ProcessStartInfo(PathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace
