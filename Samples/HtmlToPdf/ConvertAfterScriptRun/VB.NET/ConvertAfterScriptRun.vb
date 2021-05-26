Imports System
Imports BitMiracle.Docotic.Pdf.HtmlToPdf

Namespace BitMiracle.Docotic.Pdf.Samples
    Public NotInheritable Class ConvertAfterScriptRun
        Public Shared Sub Main()
            Task.Run(
                Async Function()
                    Await convertAfterScriptRun()
                End Function
        ).GetAwaiter().GetResult()
        End Sub

        Private Shared Async Function convertAfterScriptRun() As Task
            ' NOTE: 
            ' When used in trial mode, the library imposes some restrictions.
            ' Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            ' for more information.

            Dim url = New Uri("https://plugins.yithemes.com/yith-infinite-scrolling")
            Using converter = Await HtmlConverter.CreateAsync()
                ' The page at the url loads additional contents when scroll position changes.
                ' Let's run a script before the conversion. The script scrolls the page until
                ' there is no more new content. This way all the page contents will be converted.

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
                    pdf.Save("ConvertAfterScriptRun.pdf")
                End Using
            End Using

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}")
        End Function
    End Class
End Namespace
