using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class ConvertWithHeaderAndFooter
    {
        static async Task Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var pathToFile = "ConvertWithHeaderAndFooter.pdf";
            using (var converter = await HtmlConverter.CreateAsync())
            {
                var options = new HtmlConversionOptions();

                options.Page.HeaderTemplate = File.ReadAllText(@"..\Sample Data\header-template.html");
                options.Page.MarginTop = 110;

                options.Page.FooterTemplate = File.ReadAllText(@"..\Sample Data\footer-template.html");
                options.Page.MarginBottom = 50;

                var url = new Uri("https://www.iana.org/glossary");
                using var pdf = await converter.CreatePdfAsync(url, options);
                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
