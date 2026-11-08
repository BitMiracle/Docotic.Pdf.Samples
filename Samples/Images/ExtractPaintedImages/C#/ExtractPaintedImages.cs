using System;
using System.Diagnostics;
using System.Linq;

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
                var paintedImages = pdf.Pages[0].GetPaintedImages();

                PdfPaintedImage image = paintedImages.First();

                // Save the image as is.
                asIsOutputName = image.Image.Save("PdfImage.Save");

                // Save the image as painted.
                var options = new PdfPaintedImageSavingOptions
                {
                    Compression = PaintedImageCompressionOptions.CreateTiff(),
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
