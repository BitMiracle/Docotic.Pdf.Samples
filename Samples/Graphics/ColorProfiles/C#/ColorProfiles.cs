using System.Diagnostics;
using System.Drawing;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ColorProfiles
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

                PdfColorProfile colorProfile = pdf.AddColorProfile("Sample data/AdobeRGB1998.icc");
                canvas.Brush.Color = new PdfRgbColor(colorProfile, 20, 80, 240);
                canvas.DrawRectangle(new RectangleF(10, 50, 100, 70), PdfDrawMode.Fill);

                pdf.Save("ColorProfiles.pdf");
            }

            Process.Start("ColorProfiles.pdf");
        }
    }
}