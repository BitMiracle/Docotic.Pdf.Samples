Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class Metadata
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "Metadata.pdf"

            Using pdf As New PdfDocument()
                pdf.Info.Author = "Sample Browser application"
                pdf.Info.Subject = "Document metadata"
                pdf.Info.Title = "Custom title goes here"
                pdf.Info.Keywords = "pdf, Docotic.Pdf"

                Dim page As PdfPage = pdf.Pages(0)
                page.Canvas.DrawString(10, 50, "Check document properties")

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace