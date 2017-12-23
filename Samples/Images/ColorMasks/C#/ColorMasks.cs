using System.Diagnostics;
using System.Drawing;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ColorMasks
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "ColorMasks.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.Resolution = 150;

                canvas.Brush.Color = new PdfRgbColor(0, 255, 0);
                canvas.DrawRectangle(new RectangleF(50, 450, 1150, 150), PdfDrawMode.Fill);

                PdfImage image = pdf.AddImage("Sample data/pink.png", new PdfRgbColor(255, 0, 255));
                canvas.DrawImage(image, 550, 200);

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}