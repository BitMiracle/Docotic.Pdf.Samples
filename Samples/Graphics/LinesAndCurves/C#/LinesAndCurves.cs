using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class LinesAndCurves
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "LinesAndCurves.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                //draw star
                var startPosition = new PdfPoint(30, 80);
                canvas.CurrentPosition = startPosition;
                canvas.DrawLineTo(110, 80);
                canvas.DrawLineTo(45, 110);
                canvas.DrawLineTo(70, 60);
                canvas.DrawLineTo(95, 110);
                canvas.DrawLineTo(startPosition);

                //draw bezier curve
                canvas.CurrentPosition = new PdfPoint(150, 90);
                canvas.DrawCurveTo(new PdfPoint(180, 60), new PdfPoint(230, 120), new PdfPoint(250, 90));

                //draw arc
                canvas.DrawArc(new PdfPoint(350, 90), 70, 40, 30, 280);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}