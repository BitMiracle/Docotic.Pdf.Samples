using System;
using System.Diagnostics;
using System.Threading.Tasks;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class HtmlToPdfConsole
    {
        static async Task Main()
        {
            await convertUrlToPdfAsync("https://bitmiracle.com/", "HtmlToPdfConsole.pdf");
        }

        // The following shows how to use the HTML to PDF API from a regular synchronous method
        ////static void Main()
        ////{
        ////    Task.Run(async () =>
        ////    {
        ////        await convertUrlToPdfAsync("https://bitmiracle.com/", "HtmlToPdfConsole.pdf");
        ////    }).GetAwaiter().GetResult();
        ////}

        private static async Task convertUrlToPdfAsync(string urlString, string pdfFileName)
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (var converter = await HtmlConverter.CreateAsync())
            {
                using var pdf = await converter.CreatePdfAsync(new Uri(urlString));
                pdf.Save(pdfFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pdfFileName) { UseShellExecute = true });
        }
    }
}
