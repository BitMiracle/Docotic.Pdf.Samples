using System;
using System.Diagnostics;
using System.Threading.Tasks;

using BitMiracle.Docotic.Pdf.HtmlToPdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    class PutHtmlOverPdf
    {
        static async Task Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var pathToFile = "PutHtmlOverPdf.pdf";

            using (var converter = await HtmlConverter.CreateAsync())
            {
                // It is important to specify page size. Usually, the size should be equal to the size of the page
                // you want to overlay.
                var options = new HtmlConversionOptions();
                options.Page.SetSizeInches(4.13, 5.83);

                // The library produces transparent background by default. If you need an opaque background then you can
                // use CSS background like that:
                //options.Start.SetStartAfterScriptRun("document.body.style.background = 'white'");
                //
                // Alternatively, you can add a white background watermark to all converted pages.
                // Look at the addWhiteBackground method below for more detail.

                string htmlCode = 
                    "<div style=\"position: absolute; top: 270px; right: 100px;\">" +
                    "I would like to put this here</div>";
                using var htmlPdf = await converter.CreatePdfFromStringAsync(htmlCode, options);

                // Uncomment to apply semi-transparent red background:
                //addBackground(htmlPdf, new PdfRgbColor(255, 0, 0), 25);

                var pdfPath = @"..\Sample Data\simple-graphics.pdf";
                using var pdf = new PdfDocument(pdfPath);

                // Create an XObject from a page in the document generated from HTML.
                // Please note that the code uses different document instances to call the
                // method and access the collection of the pages.
                var xObj = pdf.CreateXObject(htmlPdf.Pages[0]);

                // Draw the XObject on a page from the existing PDF.
                pdf.Pages[0].Canvas.DrawXObject(xObj, 0, 0);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static void addBackground(PdfDocument pdf, PdfColor color, int opacity = 100)
        {
            PdfXObject background = pdf.CreateXObject();
            background.DrawOnBackground = true;

            bool first = true;
            foreach (PdfPage page in pdf.Pages)
            {
                if (first)
                {
                    background.Width = page.Width;
                    background.Height = page.Height;

                    if (opacity != 100)
                        background.Canvas.Brush.Opacity = opacity;

                    background.Canvas.Brush.Color = color;
                    background.Canvas.DrawRectangle(new PdfRectangle(0, 0, page.Width, page.Height), PdfDrawMode.Fill);

                    first = false;
                }

                page.Canvas.DrawXObject(background, 0, 0);
            }
        }
    }
}
