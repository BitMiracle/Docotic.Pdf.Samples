using System;
using System.Diagnostics;

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

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                double scale = pdf.Pages[0].Resolution / 150;
                canvas.ScaleTransform(scale, scale);

                canvas.Brush.Color = new PdfRgbColor(0, 255, 0);
                canvas.DrawRectangle(new PdfRectangle(50, 450, 1150, 150), PdfDrawMode.Fill);

                PdfImage? image = pdf.AddImage(@"..\Sample data\pink.png", new PdfRgbColor(255, 0, 255));
                if (image is null)
                {
                    Console.WriteLine("Cannot add image");
                    return;
                }

                canvas.DrawImage(image, 550, 200);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}