using System.Diagnostics;
using System.Drawing;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Colors
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "Colors.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                canvas.Brush.Color = new PdfRgbColor(255, 0, 0);
                canvas.Pen.Color = new PdfRgbColor(0, 255, 255);
                canvas.Pen.Width = 3;

                canvas.DrawEllipse(new RectangleF(10, 50, 200, 100), PdfDrawMode.FillAndStroke);

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}