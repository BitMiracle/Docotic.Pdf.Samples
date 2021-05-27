using System;
using System.IO;
using System.Threading.Tasks;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class ConvertLocalHtml
    {
        static async Task Main(string[] args)
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            // It is possible to use the same converter for multiple conversions.
            // When reusing the converter you save on converter instantiation time.
            using (var converter = await HtmlConverter.CreateAsync())
            {
                // Convert some HTML code in a string
                var html = "<body><br/><br/><br/><h1>Hello, World<h1></body>";
                using (var pdf = await converter.CreatePdfFromStringAsync(html))
                    pdf.Save("ConvertLocalHtmlString.pdf");

                // Convert a local HTML file
                var assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
                var sampleHtmlPath = Path.Combine(assemblyPath, "sample.html");
                using (var pdf = await converter.CreatePdfAsync(sampleHtmlPath))
                    pdf.Save("ConvertLocalHtmlFile.pdf");

                // Convert some HTML specifying a base URL
                var incompleteHtml = "<img src=\"/images/team.svg\"></img>";
                var options = new HtmlConversionOptions();
                options.Load.BaseUri = new Uri("https://bitmiracle.com/");
                using (var pdf = await converter.CreatePdfFromStringAsync(incompleteHtml, options))
                    pdf.Save("ConvertHtmlWithBaseUrl.pdf");
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
