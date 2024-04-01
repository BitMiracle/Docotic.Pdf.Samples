using System;
using System.Diagnostics;
using System.Threading.Tasks;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class ConvertLocalHtml
    {
        static async Task Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            var htmlStringOutputName = "ConvertLocalHtmlString.pdf";
            var htmlFileOutputName = "ConvertLocalHtmlFile.pdf";
            var htmlWithBaseUrlOutputName = "ConvertHtmlWithBaseUrl.pdf";

            // It is possible to use the same converter for multiple conversions.
            // When reusing the converter you save on converter instantiation time.
            using (var converter = await HtmlConverter.CreateAsync())
            {
                // Convert some HTML code in a string
                var html = "<body><br/><br/><br/><h1>Hello, World</h1></body>";
                using (var pdf = await converter.CreatePdfFromStringAsync(html))
                    pdf.Save(htmlStringOutputName);

                // Convert a local HTML file
                using (var pdf = await converter.CreatePdfAsync(@"..\Sample Data\sample.html"))
                    pdf.Save(htmlFileOutputName);

                // Convert some HTML specifying a base URL
                var incompleteHtml = "<img src=\"/images/team.svg\"></img>";
                var options = new HtmlConversionOptions();
                options.Load.BaseUri = new Uri("https://bitmiracle.com/");
                using (var pdf = await converter.CreatePdfFromStringAsync(incompleteHtml, options))
                    pdf.Save(htmlWithBaseUrlOutputName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(htmlStringOutputName) { UseShellExecute = true });
            Process.Start(new ProcessStartInfo(htmlFileOutputName) { UseShellExecute = true });
            Process.Start(new ProcessStartInfo(htmlWithBaseUrlOutputName) { UseShellExecute = true });
        }
    }
}
