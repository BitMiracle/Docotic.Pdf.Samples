using System;
using System.Diagnostics;
using System.Threading.Tasks;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class ConvertPasswordProtected
    {
        static async Task Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var pathToFile = "ConvertPasswordProtected.pdf";

            using (var converter = await HtmlConverter.CreateAsync())
            {
                var url = new Uri("http://httpbin.org/basic-auth/foo/bar");

                var options = new HtmlConversionOptions();
                options.Authentication.SetCredentials("foo", "bar");

                // The following two lines are here just to make the output easier to read.
                options.Page.MarginTop = 100;
                options.Page.Scale = 2;

                using (var pdf = await converter.CreatePdfAsync(url, options))
                    pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
