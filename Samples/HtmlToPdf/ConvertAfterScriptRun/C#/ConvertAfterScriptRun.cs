using System;
using System.Diagnostics;
using System.Threading.Tasks;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class ConvertAfterScriptRun
    {
        static async Task Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var url = new Uri("https://infinite-scroll.com/demo/full-page/");
            var pathToFile = "ConvertAfterScriptRun.pdf";
            using (var converter = await HtmlConverter.CreateAsync())
            {
                // The page at the url loads additional contents when scroll position changes.
                // Let's run a script before the conversion. The script scrolls the page until
                // there is no more new content. This way all the page contents will be converted.

                var options = new HtmlConversionOptions();
                var js = @"
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
                ";
                options.Start.SetStartAfterScriptRun(js);

                using var pdf = await converter.CreatePdfAsync(url, options);
                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
