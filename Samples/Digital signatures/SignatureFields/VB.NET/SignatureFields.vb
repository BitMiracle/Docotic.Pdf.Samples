Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    NotInheritable Class SignatureFields
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "SignatureFields.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                ' creates a signature field with an auto-generated name
                page.AddSignatureField(100, 100, 200, 50)

                ' creates a signature field with the specified name
                page.AddSignatureField("Signature2", 100, 200, 200, 50)

                ' creates an invisible signature with the specified name
                ' please note the width And height are zero for an invisible signature
                page.AddSignatureField("InvisibleSignature", 350, 100, 0, 0)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace