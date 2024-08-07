using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RemovePaintedImages
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            const string PathToFile = "RemovePaintedImages.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\ImageScaleAndRotate.pdf"))
            {
                PdfPage page = pdf.Pages[0];
                page.RemovePaintedImages(
                    image =>
                    {
                        // remove rotated images
                        PdfMatrix m = image.TransformationMatrix;
                        return Math.Abs(m.M12) > 0.001 || Math.Abs(m.M21) > 0.001;
                    }
                );

                pdf.Save(PathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }
}
