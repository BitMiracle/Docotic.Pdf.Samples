Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class HtmlToPdfConsole
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await ConvertUrlToPdfAsync("https://bitmiracle.com/", "HtmlToPdfConsole.pdf")
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function ConvertUrlToPdfAsync(urlString As String, pdfFileName As String) As Task
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Using converter = Await HtmlConverter.CreateAsync()

                Using pdf = Await converter.CreatePdfAsync(New Uri(urlString))
                    pdf.Save(pdfFileName)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pdfFileName) With {.UseShellExecute = True})
        End Function
    End Class
End Namespace