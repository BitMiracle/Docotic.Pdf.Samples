using System;
using System.Diagnostics;
using System.Threading.Tasks;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class ConvertWithDelay
    {
        static async Task Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var pathToFile = "ConvertWithDelay.pdf";

            // Wait for 10 seconds before starting the conversion.
            // This should be enough for the test to complete.
            var options = new HtmlConversionOptions();
            options.Start.SetStartAfterDelay(10 * 1000);

            using (var converter = await HtmlConverter.CreateAsync())
            {
                using var pdf = await converter.CreatePdfAsync(new Uri("http://acid3.acidtests.org/"), options);
                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
