using System;
using System.Drawing;
using System.IO;
using System.Reflection;

using BitMiracle.Docotic.Pdf.Gdi;

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

            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using (PdfDocument pdf = new PdfDocument(Path.Combine(location, "jfif3.pdf")))
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

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
