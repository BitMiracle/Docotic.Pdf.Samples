Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class OpenDocument
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "OpenDocument.pdf"

            Using pdf As New PdfDocument("..\Sample data\jfif3.pdf")
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                canvas.Font = pdf.CreateFont(PdfBuiltInFont.Helvetica)
                canvas.FontSize = 20
                canvas.DrawString(10, 80, "This text was added by Docotic.Pdf")

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace