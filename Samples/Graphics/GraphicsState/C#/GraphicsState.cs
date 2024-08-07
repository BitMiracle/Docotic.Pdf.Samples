using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class GraphicsState
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "GraphicsState.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.SaveState();

                canvas.Pen.Width = 3;
                canvas.Pen.DashPattern = new PdfDashPattern(new double[] { 5, 3 }, 2);

                canvas.CurrentPosition = new PdfPoint(20, 60);
                canvas.DrawLineTo(150, 60);

                canvas.RestoreState();

                canvas.CurrentPosition = new PdfPoint(20, 80);
                canvas.DrawLineTo(150, 80);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}