using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddRecompressedImages
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            string pathToFile = "AddRecompressedImages.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                PdfImageFrames? imageFrames = pdf.OpenImage(@"..\Sample Data\pink.png");
                if (imageFrames is null)
                {
                    Console.WriteLine("Cannot add image");
                    return;
                }

                PdfImage originalImage = pdf.AddImage(imageFrames[0]);
                canvas.DrawImage(originalImage, 10, 10, 0);

                PdfImageFrames? imageFramesToRecompress = pdf.OpenImage(@"..\Sample Data\pink.png");
                if (imageFramesToRecompress is null)
                {
                    Console.WriteLine("Cannot add image");
                    return;
                }

                PdfImageFrame frame = imageFramesToRecompress[0];
                frame.OutputCompression = PdfImageCompression.Jpeg;
                frame.JpegQuality = 50;
                frame.RecompressAlways = true;

                PdfImage compressedImage = pdf.AddImage(frame);
                canvas.DrawImage(compressedImage, 210, 10, 0);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
