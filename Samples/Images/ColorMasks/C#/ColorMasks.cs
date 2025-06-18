using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ColorMasks
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "ColorMasks.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                double scale = pdf.Pages[0].Resolution / 150;
                canvas.ScaleTransform(scale, scale);

                canvas.Brush.Color = new PdfRgbColor(0, 255, 0);
                canvas.DrawRectangle(new PdfRectangle(50, 450, 1150, 150), PdfDrawMode.Fill);

                PdfImage image = pdf.CreateImage(@"..\Sample data\pink.png");
                image.MakeTransparent(new PdfRgbColor(255, 0, 255));

                canvas.DrawImage(image, 550, 200);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}