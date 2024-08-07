using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Shapes
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var pathToFile = "Shapes.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                canvas.DrawCircle(new PdfPoint(60, 100), 40);
                canvas.DrawEllipse(new PdfRectangle(10, 150, 100, 50));
                canvas.DrawRectangle(new PdfRectangle(160, 80, 110, 50));
                canvas.DrawRoundedRectangle(new PdfRectangle(160, 150, 110, 50), new PdfSize(30, 30));

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}