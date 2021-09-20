using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class ConvertWithHeaderAndFooter
    {
        static async Task Main(string[] args)
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (var converter = await HtmlConverter.CreateAsync())
            {
                var options = new HtmlConversionOptions();

                var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                options.Page.HeaderTemplate = File.ReadAllText(Path.Combine(location, "header-template.html"));
                options.Page.MarginTop = 110;

                options.Page.FooterTemplate = File.ReadAllText(Path.Combine(location, "footer-template.html"));
                options.Page.MarginBottom = 50;

                var url = new Uri("https://www.iana.org/glossary");
                using (var pdf = await converter.CreatePdfAsync(url, options))
                    pdf.Save("ConvertWithHeaderAndFooter.pdf");
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
