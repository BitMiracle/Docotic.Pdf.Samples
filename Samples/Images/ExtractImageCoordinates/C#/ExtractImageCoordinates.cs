using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractImageCoordinates
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            string pathToFile = "ExtractImageCoordinates.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\gmail-cheat-sheet.pdf"))
            {
                foreach (PdfPage page in pdf.Pages)
                {
                    foreach (PdfPaintedImage image in page.GetPaintedImages())
                    {
                        PdfCanvas canvas = page.Canvas;
                        canvas.Pen.Width = 3;
                        canvas.Pen.Color = new PdfRgbColor(255, 0, 0);

                        canvas.DrawRectangle(image.Bounds);
                    }
                }
                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
