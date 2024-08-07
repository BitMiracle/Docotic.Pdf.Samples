Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class TextRise
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "TextRise.pdf"

            Using pdf As New PdfDocument()

                Const sampleText As String = "Hello!"
                Dim canvas As PdfCanvas = pdf.Pages(0).Canvas
                canvas.TextPosition = New PdfPoint(10, 50)

                canvas.TextRise = 0
                canvas.DrawString(sampleText)
                canvas.TextRise = 5
                canvas.DrawString(sampleText)
                canvas.TextRise = -10
                canvas.DrawString(sampleText)

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace