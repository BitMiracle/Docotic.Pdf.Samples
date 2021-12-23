using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Clipping
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "Clipping.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.Pen.Width = 2;
                canvas.Brush.Color = new PdfRgbColor(255, 0, 0);

                drawStar(canvas);

                canvas.SetClip(PdfFillMode.Alternate);
                canvas.StrokePath();

                canvas.DrawRectangle(new PdfRectangle(0, 0, 500, 500), PdfDrawMode.Fill);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }

        private static void drawStar(PdfCanvas canvas)
        {
            canvas.CurrentPosition = new PdfPoint(10, 100);
            canvas.AppendLineTo(110, 100);
            canvas.AppendLineTo(30, 160);
            canvas.AppendLineTo(60, 70);
            canvas.AppendLineTo(80, 160);
            canvas.ClosePath();
        }
    }
}