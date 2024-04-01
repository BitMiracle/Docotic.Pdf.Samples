Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ProtectDocumentWithAes
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim pathToFile As String = "ProtectDocumentWithAes.pdf"

            Using pdf As New PdfDocument()
                pdf.Pages(0).Canvas.DrawString("Hello World!")

                Dim saveOptions As New PdfSaveOptions With
                {
                    .EncryptionHandler = New PdfStandardEncryptionHandler(String.Empty, "user") With
                    {
                        .Algorithm = PdfEncryptionAlgorithm.Aes256Bit
                    }
                }
                pdf.Save(pathToFile, saveOptions)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace