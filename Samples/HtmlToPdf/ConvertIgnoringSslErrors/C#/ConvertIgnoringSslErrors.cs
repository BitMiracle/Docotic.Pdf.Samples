using System;
using System.Diagnostics;
using System.Threading.Tasks;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class ConvertIgnoringSslErrors
    {
        static async Task Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var pathToFile = "ConvertIgnoringSslErrors.pdf";

            var engineOptions = new HtmlEngineOptions
            {
                IgnoreSslErrors = true
            };
            using (var converter = await HtmlConverter.CreateAsync(engineOptions))
            {
                var url = new Uri("https://self-signed.badssl.com/");
                using var pdf = await converter.CreatePdfAsync(url);
                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
