Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ConvertWithMarginsAndScale
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await ConvertWithMarginsAndScale()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function ConvertWithMarginsAndScale() As Task
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "ConvertWithMarginsAndScale.pdf"

            Using converter = Await HtmlConverter.CreateAsync()
                Dim html = "<html><head><style>body { background-color: coral; margin-top: 100px;}</style></head>" &
                    "<body><h1>Did you notice the margins and the scale?</h1></body></html>"

                Dim options = New HtmlConversionOptions()
                options.Page.MarginLeft = 10
                options.Page.MarginTop = 20
                options.Page.MarginRight = 30
                options.Page.MarginBottom = 40
                options.Page.Scale = 1.5

                Using pdf = Await converter.CreatePdfFromStringAsync(html, options)
                    pdf.Save(pathToFile)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Function
    End Class
End Namespace
