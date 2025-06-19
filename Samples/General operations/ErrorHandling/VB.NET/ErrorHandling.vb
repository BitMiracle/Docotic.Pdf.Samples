Imports BitMiracle.Docotic
Imports BitMiracle.Docotic.Pdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ErrorHandling
        Public Shared Sub Main()
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "ErrorHandling.pdf"

            Using pdf As New PdfDocument()
                Dim page As PdfPage = pdf.Pages(0)

                Try
                    ' tries to draw Russian string with built-in Helvetica font, which doesn't
                    ' define glyphs for Russian characters. So, we expect that a PdfException will be thrown.
                    page.Canvas.DrawString(10, 50, "Привет, мир!")
                Catch ex As CannotShowTextException
                    page.Canvas.Font = pdf.CreateFont("Arial")
                    page.Canvas.DrawString(10, 50, "The expected exception occurred:")
                    page.Canvas.DrawString(10, 65, ex.Message)
                End Try

                pdf.Save(pathToFile)
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Sub
    End Class
End Namespace