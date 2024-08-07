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
                pdf.AddPage();

                PdfXObject watermark = pdf.CreateXObject();

                // uncomment following line if you want the watermark to be placed before
                // any other contents of a page.
                // watermark.DrawOnBackground = true;

                PdfCanvas watermarkCanvas = watermark.Canvas;
                watermarkCanvas.TextAngle = -45;
                watermarkCanvas.FontSize = 36;
                watermarkCanvas.DrawString(100, 100, "This text is a part of the watermark");

                foreach (PdfPage page in pdf.Pages)
                {
                    // draw watermark at the top left corner over the page contents
                    // this will also add watermark to the collection of page XObjects
                    page.Canvas.DrawXObject(watermark, 0, 0);
                }

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}