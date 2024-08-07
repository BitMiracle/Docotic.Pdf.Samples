using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Paths
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Paths.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.Pen.Width = 3;
                canvas.Pen.Color = new PdfRgbColor(78, 55, 150);
                canvas.Brush.Color = new PdfRgbColor(200, 55, 50);

                canvas.AppendRectangle(new PdfRectangle(10, 60, 100, 100));
                // following call resets the path and rectangle won't be drawn
                canvas.ResetPath();

                canvas.CurrentPosition = new PdfPoint(20, 70);
                canvas.AppendLineTo(100, 70);
                canvas.AppendLineTo(100, 110);
                canvas.AppendCurveTo(new PdfPoint(80, 90), new PdfPoint(50, 90), new PdfPoint(30, 110));
                canvas.FillAndStrokePath(PdfFillMode.Winding);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}