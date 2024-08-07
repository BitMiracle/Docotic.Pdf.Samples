Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ConvertWithDelay
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await ConvertWithDelay()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function ConvertWithDelay() As Task
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "ConvertWithDelay.pdf"

            ' Wait for 10 seconds before starting the conversion.
            ' This should be enough for the test to complete.
            Dim options = New HtmlConversionOptions()
            options.Start.SetStartAfterDelay(10 * 1000)

            Using converter = Await HtmlConverter.CreateAsync()
                Using pdf = Await converter.CreatePdfAsync(New Uri("http://acid3.acidtests.org/"), options)
                    pdf.Save(pathToFile)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Function
    End Class
End Namespace
