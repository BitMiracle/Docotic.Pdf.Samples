using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Colors
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            string pathToFile = "Colors.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                canvas.Brush.Color = new PdfRgbColor(255, 0, 0);
                canvas.Pen.Color = new PdfRgbColor(0, 255, 255);
                canvas.Pen.Width = 3;

                canvas.DrawEllipse(new PdfRectangle(10, 50, 200, 100), PdfDrawMode.FillAndStroke);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}