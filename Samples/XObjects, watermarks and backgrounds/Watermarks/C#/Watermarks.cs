using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Watermarks
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Watermarks.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.Pages.Add();

                PdfXObject watermark = pdf.CreateXObject();

                // Uncomment the following line to place the watermark behind all other page content.
                // watermark.DrawOnBackground = true;

                PdfCanvas watermarkCanvas = watermark.Canvas;
                watermarkCanvas.TextAngle = -45;
                watermarkCanvas.FontSize = 36;
                watermarkCanvas.DrawString(100, 100, "This text is a part of the watermark");

                foreach (var page in pdf.Pages)
                {
                    // Draw the watermark over the page contents, starting at the top-left corner.
                    // The watermark will also be added to the page's XObjects collection.
                    page?.Canvas.DrawXObject(watermark, 0, 0);
                }

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}