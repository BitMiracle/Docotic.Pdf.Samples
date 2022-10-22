using System;
using System.Diagnostics;
using System.Threading.Tasks;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class PutHtmlOverPdf
    {
        static async Task Main(string[] args)
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var pathToFile = "PutHtmlOverPdf.pdf";

            using (var converter = await HtmlConverter.CreateAsync())
            {
                // It is important to specify page size and turn on transparent background.
                // Usually, the size should be equal to the size of the page you want to overlay.
                var options = new HtmlConversionOptions();
                options.Page.TransparentBackground = true;
                options.Page.SetSizeInches(4.13, 5.83);

                string htmlCode = 
                    "<div style=\"position: absolute; top: 270px; right: 100px;\">" +
                    "I would like to put this here</div>";
                using (var htmlPdf = await converter.CreatePdfFromStringAsync(htmlCode, options))
                {
                    var pdfPath = @"..\Sample Data\simple-graphics.pdf";
                    using (var pdf = new PdfDocument(pdfPath))
                    {
                        // Create an XObject from a page in the document generated from HTML.
                        // Please note that the code uses different document instances to call the
                        // method and access the collection of the pages.
                        var xObj = pdf.CreateXObject(htmlPdf.Pages[0]);

                        // Draw the XObject on a page from the existing PDF.
                        pdf.Pages[0].Canvas.DrawXObject(xObj, 0, 0);

                        pdf.Save(pathToFile);
                    }
                }
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
