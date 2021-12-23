using System;

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

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                PdfFont helvetica = pdf.AddFont(PdfBuiltInFont.HelveticaBoldOblique, false, false);
                canvas.Font = helvetica;
                canvas.FontSize = 28;

                drawColoredPattern(pdf);
                drawUncoloredPattern(pdf);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }

        private static void drawColoredPattern(PdfDocument pdf)
        {
            PdfTilingPattern pattern = pdf.AddColoredPattern(5, 5);
            fill(pattern);

            PdfCanvas canvas = pdf.GetPage(0).Canvas;
            canvas.Brush.Pattern = pattern;
            canvas.DrawString(new PdfPoint(50, 50), 0, "Pattern-filled text");
            canvas.DrawCircle(new PdfPoint(0, 0), 20, PdfDrawMode.FillAndStroke);
        }

        private static void drawUncoloredPattern(PdfDocument pdf)
        {
            PdfTilingPattern pattern = pdf.AddUncoloredPattern(5, 5, new PdfRgbColorSpace());
            fill(pattern);

            PdfCanvas canvas = pdf.GetPage(0).Canvas;
            canvas.Brush.Pattern = pattern;
            canvas.Brush.Color = new PdfRgbColor(0, 255, 0);
            canvas.DrawString(new PdfPoint(50, 150), "Uncolored Pattern colored green");
        }

        private static void fill(PdfTilingPattern pattern)
        {
            PdfCanvas canvas = pattern.Canvas;
            PdfColor red = new PdfRgbColor(255, 0, 0);
            canvas.Brush.Color = red;
            canvas.Pen.Color = red;
            canvas.AppendCircle(new PdfPoint(2, 2), 2);
            canvas.FillAndStrokePath(PdfFillMode.Winding);
        }
    }
}