using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Versioning;
using BitMiracle.Docotic.Pdf.Gdi;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class DrawPageOnGraphics
    {
        [SupportedOSPlatform("windows")]
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.
            
            string pathToImage = "DrawPageOnGraphics.png";

            using (var pdf = new PdfDocument(@"..\Sample Data\jfif3.pdf"))
            {
                const float TargetResolution = 300;

                PdfPage page = pdf.Pages[0];
                double scaleFactor = TargetResolution / page.Resolution;

                using var bitmap = new Bitmap((int)(page.Width * scaleFactor), (int)(page.Height * scaleFactor));
                bitmap.SetResolution(TargetResolution, TargetResolution);

                using Graphics gr = Graphics.FromImage(bitmap);
                page.Draw(gr);

                bitmap.Save(pathToImage);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToImage) { UseShellExecute = true });
        }
    }
}
