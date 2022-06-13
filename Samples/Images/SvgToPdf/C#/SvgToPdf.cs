using System;
using System.Threading.Tasks;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class SvgToPdf
    {
        static async Task Main(string[] args)
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (var converter = await HtmlConverter.CreateAsync())
            {
                using (var pdf = await converter.CreatePdfAsync(@"..\Sample Data\image.svg"))
                    pdf.Save("SvgToPdf.pdf");
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
