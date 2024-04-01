Imports BitMiracle.Docotic.Pdf.Layout

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class PdfDocumentBuilderSample
        Public Shared Sub Main()
            ' NOTE:
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

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
