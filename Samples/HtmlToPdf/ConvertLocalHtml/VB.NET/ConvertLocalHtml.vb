Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ConvertLocalHtml
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await ConvertLocalHtml()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function ConvertLocalHtml() As Task
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            ' for more information.

            Dim htmlStringOutputPath As String = "ConvertLocalHtmlString.pdf"
            Dim htmlFileOutputPath As String = "ConvertLocalHtmlFile.pdf"
            Dim htmlWithBaseUrlOutputPath As String = "ConvertHtmlWithBaseUrl.pdf"

            ' It is possible to use the same converter for multiple conversions.
            ' When reusing the converter you save on converter instantiation time.
            Using converter = Await HtmlConverter.CreateAsync()
                ' Convert some HTML code in a string
                Dim html = "<body><br/><br/><br/><h1>Hello, World<h1></body>"
                Using pdf = Await converter.CreatePdfFromStringAsync(html)
                    pdf.Save(htmlStringOutputPath)
                End Using

                ' Convert a local HTML file
                Using pdf = Await converter.CreatePdfAsync("..\Sample Data\sample.html")
                    pdf.Save(htmlFileOutputPath)
                End Using

                ' Convert some HTML specifying a base URL
                Dim incompleteHtml = "<img src=""/images/team.svg""></img>"
                Dim options = New HtmlConversionOptions()
                options.Load.BaseUri = New Uri("https://bitmiracle.com/")
                Using pdf = Await converter.CreatePdfFromStringAsync(incompleteHtml, options)
                    pdf.Save(htmlWithBaseUrlOutputPath)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(htmlStringOutputPath) With {.UseShellExecute = True})
            Process.Start(New ProcessStartInfo(htmlFileOutputPath) With {.UseShellExecute = True})
            Process.Start(New ProcessStartInfo(htmlWithBaseUrlOutputPath) With {.UseShellExecute = True})
        End Function
    End Class
End Namespace
