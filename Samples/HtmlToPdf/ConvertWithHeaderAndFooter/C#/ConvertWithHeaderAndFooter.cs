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
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var pathToFile = "ConvertWithHeaderAndFooter.pdf";
            using (var converter = await HtmlConverter.CreateAsync())
            {
                var options = new HtmlConversionOptions();

                options.Page.HeaderTemplate = File.ReadAllText(@"..\Sample Data\header-template.html");
                options.Page.MarginTop = 110;

                options.Page.FooterTemplate = File.ReadAllText(@"..\Sample Data\footer-template.html");
                options.Page.MarginBottom = 50;

                var url = new Uri("https://www.iana.org/glossary");
                using (var pdf = await converter.CreatePdfAsync(url, options))
                    pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
