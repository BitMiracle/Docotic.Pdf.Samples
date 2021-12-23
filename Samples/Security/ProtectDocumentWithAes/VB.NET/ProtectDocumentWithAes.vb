Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ProtectDocumentWithAes
        Public Shared Sub Main()
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim pathToFile As String = "ProtectDocumentWithAes.pdf"

            Using pdf As New PdfDocument()
                pdf.Pages(0).Canvas.DrawString("Hello World!")

                Dim handler = New PdfStandardEncryptionHandler(String.Empty, "user")
                handler.Algorithm = PdfEncryptionAlgorithm.Aes256Bit
                pdf.SaveOptions.EncryptionHandler = handler

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Sub
    End Class
End Namespace