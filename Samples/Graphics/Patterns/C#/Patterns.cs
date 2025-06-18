using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Patterns
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Patterns.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.Font = pdf.CreateFont(PdfBuiltInFont.HelveticaBoldOblique);
                canvas.FontSize = 28;

                DrawColoredPattern(pdf);
                DrawUncoloredPattern(pdf);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static void DrawColoredPattern(PdfDocument pdf)
        {
            PdfTilingPattern pattern = pdf.CreateColoredPattern(5, 5);
            Fill(pattern);

            PdfCanvas canvas = pdf.GetPage(0).Canvas;
            canvas.Brush.Pattern = pattern;
            canvas.DrawString(new PdfPoint(50, 50), "Pattern-filled text");
            canvas.DrawCircle(new PdfPoint(0, 0), 20, PdfDrawMode.FillAndStroke);
        }

        private static void DrawUncoloredPattern(PdfDocument pdf)
        {
            PdfTilingPattern pattern = pdf.CreateUncoloredPattern(5, 5, new PdfRgbColorSpace());
            Fill(pattern);

            PdfCanvas canvas = pdf.GetPage(0).Canvas;
            canvas.Brush.Pattern = pattern;
            canvas.Brush.Color = new PdfRgbColor(0, 255, 0);
            canvas.DrawString(new PdfPoint(50, 150), "Uncolored Pattern colored green");
        }

        private static void Fill(PdfTilingPattern pattern)
        {
            PdfCanvas canvas = pattern.Canvas;
            var red = new PdfRgbColor(255, 0, 0);
            canvas.Brush.Color = red;
            canvas.Pen.Color = red;
            canvas.AppendCircle(new PdfPoint(2, 2), 2);
            canvas.FillAndStrokePath(PdfFillMode.Winding);
        }
    }
}