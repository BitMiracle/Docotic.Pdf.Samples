using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractPaintedImages
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var asIsOutputName = "";
            var asPaintedOutputName = "PdfPaintedImage.SaveAsPainted.tiff";

            using (var pdf = new PdfDocument(@"..\Sample Data\ImageScaleAndRotate.pdf"))
            {
                PdfCollection<PdfPaintedImage> paintedImages = pdf.Pages[0].GetPaintedImages();

                PdfPaintedImage image = paintedImages[0];

                // save image as is
                asIsOutputName = image.Image.Save("PdfImage.Save");

                // save image as painted
                var options = new PdfPaintedImageSavingOptions
                {
                    Format = PdfExtractedImageFormat.Tiff,
                    HorizontalResolution = 300,
                    VerticalResolution = 300
                };
                image.SaveAsPainted(asPaintedOutputName, options);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(asIsOutputName) { UseShellExecute = true });
            Process.Start(new ProcessStartInfo(asPaintedOutputName) { UseShellExecute = true });
        }
    }
}
