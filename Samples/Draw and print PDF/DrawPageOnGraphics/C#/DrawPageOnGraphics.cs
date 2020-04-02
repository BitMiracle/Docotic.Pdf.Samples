using System.Diagnostics;
using System.Drawing;

using BitMiracle.Docotic.Pdf;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class DrawPageOnGraphics
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            string pathToImage = "DrawPageOnGraphics.png";

            using (PdfDocument pdf = new PdfDocument(@"Sample Data\jfif3.pdf"))
            {
                const float TargetResolution = 300;
                
                PdfPage page = pdf.Pages[0];
                double scaleFactor = TargetResolution / page.Resolution;

                using (Bitmap bitmap = new Bitmap((int)(page.Width * scaleFactor), (int)(page.Height * scaleFactor)))
                {
                    bitmap.SetResolution(TargetResolution, TargetResolution);

                    using (Graphics gr = Graphics.FromImage(bitmap))
                        page.Draw(gr);

                    bitmap.Save(pathToImage);
                }
            }

            Process.Start(pathToImage);
        }
    }
}
