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
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var pathToFile = "PutHtmlOverPdf.pdf";

            using (var converter = await HtmlConverter.CreateAsync())
            {
                // Specify the page size. It should usually match the size of the page that you
                // want to overlay.
                var options = new HtmlConversionOptions();
                options.Page.SetSizeInches(4.13, 5.83);

                // The library uses a transparent background by default. To make the background
                // opaque, you can set it using CSS, for example:
                // options.Start.SetStartAfterScriptRun("document.body.style.background = 'white'");
                //
                // Alternatively, you can add a white background watermark to every converted page.
                // See the addWhiteBackground method below for an example.

                string htmlCode =
                    "<div style=\"position: absolute; top: 270px; right: 100px;\">" +
                    "I would like to put this here</div>";
                using var htmlPdf = await converter.CreatePdfFromStringAsync(htmlCode, options);

                // Uncomment to add a semi-transparent red background:
                //AddBackground(htmlPdf, new PdfRgbColor(255, 0, 0), 25);

                var pdfPath = @"..\Sample Data\simple-graphics.pdf";
                using var pdf = new PdfDocument(pdfPath);

                // Create an XObject from the first page of the HTML-generated PDF.
                // Note that the page and the method are accessed through different
                // PdfDocument instances.
                var xObj = pdf.CreateXObject(htmlPdf.Pages[0]);

                // Draw the XObject on a page of the existing PDF document.
                pdf.Pages[0].Canvas.DrawXObject(xObj, 0, 0);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static void AddBackground(PdfDocument pdf, PdfColor color, int opacity = 100)
        {
            PdfXObject background = pdf.CreateXObject();
            background.DrawOnBackground = true;

            bool first = true;
            foreach (var page in pdf.Pages)
            {
                if (page == null)
                    continue;

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
