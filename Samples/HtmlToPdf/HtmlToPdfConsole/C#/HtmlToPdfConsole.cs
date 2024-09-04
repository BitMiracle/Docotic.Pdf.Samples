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
            await ConvertUrlToPdfAsync("https://bitmiracle.com/", "HtmlToPdfConsole.pdf");
        }

#if NO_ASYNC_MAIN
        // The following shows how to use the HTML to PDF API from a regular synchronous method
        static void Main()
        {
            Task.Run(async () =>
            {
                await ConvertUrlToPdfAsync("https://bitmiracle.com/", "HtmlToPdfConsole.pdf");
            }).GetAwaiter().GetResult();
        }
#endif

        private static async Task ConvertUrlToPdfAsync(string urlString, string pdfFileName)
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

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
