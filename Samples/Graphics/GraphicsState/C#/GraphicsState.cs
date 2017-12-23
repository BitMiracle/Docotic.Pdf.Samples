using System.Diagnostics;
using System.Drawing;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class GraphicsState
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "GraphicsState.pdf";

            using (PdfDocument pdf = new PdfDocument())
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

            Process.Start(pathToFile);
        }
    }
}