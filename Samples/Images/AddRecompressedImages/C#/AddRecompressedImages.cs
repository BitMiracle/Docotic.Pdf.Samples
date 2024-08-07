using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddRecompressedImages
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

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
