using System;
using System.Diagnostics;
using System.Threading.Tasks;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class ConvertWithMarginsAndScale
    {
        static async Task Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var pathToFile = "ConvertWithMarginsAndScale.pdf";

            using (var converter = await HtmlConverter.CreateAsync())
            {
                var html = "<html><head><style>body { background-color: coral; margin-top: 100px;}</style></head>" +
                "<body><h1>Did you notice the margins and the scale?</h1></body></html>";

                var options = new HtmlConversionOptions();
                options.Page.MarginLeft = 10;
                options.Page.MarginTop = 20;
                options.Page.MarginRight = 30;
                options.Page.MarginBottom = 40;
                options.Page.Scale = 1.5;

                using var pdf = await converter.CreatePdfFromStringAsync(html, options);
                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
