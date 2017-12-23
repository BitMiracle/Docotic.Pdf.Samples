using System.Diagnostics;
using System.Drawing;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Shapes
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                canvas.DrawCircle(new PointF(60, 100), 40);
                canvas.DrawEllipse(new RectangleF(10, 150, 100, 50));
                canvas.DrawRectangle(new RectangleF(160, 80, 110, 50));
                canvas.DrawRoundedRectangle(new RectangleF(160, 150, 110, 50), new SizeF(30, 30));

                pdf.Save("Shapes.pdf");
            }

            Process.Start("Shapes.pdf");
        }
    }
}