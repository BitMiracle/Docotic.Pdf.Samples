using System.Diagnostics;
using System.Drawing;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class DrawText
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "DrawText.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                canvas.DrawString(10, 50, "Hello, world!");

                const string longString = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
                canvas.DrawString(longString, new RectangleF(10, 70, 40, 150), PdfTextAlign.Left, PdfVerticalAlign.Top);
                canvas.DrawText(longString, new RectangleF(70, 70, 40, 150), PdfTextAlign.Left, PdfVerticalAlign.Top);

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}