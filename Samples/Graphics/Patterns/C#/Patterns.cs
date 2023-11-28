using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Patterns
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "Patterns.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                PdfFont helvetica = pdf.AddFont(PdfBuiltInFont.HelveticaBoldOblique, false, false);
                canvas.Font = helvetica;
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
            PdfTilingPattern pattern = pdf.AddColoredPattern(5, 5);
            Fill(pattern);

            PdfCanvas canvas = pdf.GetPage(0).Canvas;
            canvas.Brush.Pattern = pattern;
            canvas.DrawString(new PdfPoint(50, 50), "Pattern-filled text");
            canvas.DrawCircle(new PdfPoint(0, 0), 20, PdfDrawMode.FillAndStroke);
        }

        private static void DrawUncoloredPattern(PdfDocument pdf)
        {
            PdfTilingPattern pattern = pdf.AddUncoloredPattern(5, 5, new PdfRgbColorSpace());
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