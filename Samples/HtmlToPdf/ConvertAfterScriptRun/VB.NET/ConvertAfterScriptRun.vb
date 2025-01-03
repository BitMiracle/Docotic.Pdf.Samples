Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ConvertAfterScriptRun
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await ConvertAfterScriptRun()
                End Function
            ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function ConvertAfterScriptRun() As Task
            ' NOTE:
            ' Without a license, the library won't allow you to create or read PDF documents.
            ' To get a free time-limited license key, use the form on
            ' https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE")

            Dim pathToFile As String = "ConvertAfterScriptRun.pdf"

            Dim url = New Uri("https://infinite-scroll.com/demo/full-page/")
            Using converter = Await HtmlConverter.CreateAsync()
                ' The page at the url loads additional contents when scroll position changes.
                ' Let's run a script before the conversion. The script scrolls down the page until
                ' there is no more new content. This way, all the page contents will be converted.

                Dim options = New HtmlConversionOptions()
                Dim js = "
                    async function scrollDownUntilYouCantAnyMore() {
                        await new Promise((resolve, reject) => {
                            var totalHeight = 0;
                            var distance = 100;
                            var timer = setInterval(() => {
                                var scrollHeight = document.body.scrollHeight;
                                window.scrollBy(0, distance);
                                totalHeight += distance;

                                if(totalHeight >= scrollHeight){
                                    clearInterval(timer);
                                    resolve();
                                }
                            }, 400);
                        });
                    }

                    scrollDownUntilYouCantAnyMore();
                "
                options.Start.SetStartAfterScriptRun(js)

                Using pdf = Await converter.CreatePdfAsync(url, options)
                    pdf.Save(pathToFile)
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")

            Process.Start(New ProcessStartInfo(pathToFile) With {.UseShellExecute = True})
        End Function
    End Class
End Namespace
