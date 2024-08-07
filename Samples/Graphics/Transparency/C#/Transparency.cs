using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Transparency
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Transparency.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                var rect = new PdfRectangle(50, 50, 50, 50);

                canvas.Brush.Opacity = 100;
                canvas.Pen.Opacity = 100;
                canvas.Brush.Color = new PdfRgbColor(200, 140, 7);
                canvas.DrawRectangle(rect, 0, PdfDrawMode.Fill);

                canvas.Brush.Opacity = 40;
                canvas.Brush.Color = new PdfRgbColor(125, 125, 255);
                rect.Location = new PdfPoint(rect.X + 15, rect.Y + 15);
                canvas.DrawRectangle(rect, 0, PdfDrawMode.Fill);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
};