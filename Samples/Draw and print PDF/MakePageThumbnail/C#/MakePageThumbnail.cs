using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class MakePageThumbnail
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToImage = "MakePageThumbnail.png";

            using (var pdf = new PdfDocument(@"..\Sample Data\jfif3.pdf"))
            {
                PdfDrawOptions options = PdfDrawOptions.CreateFitSize(new PdfSize(200, 200), false);
                options.BackgroundColor = new PdfGrayColor(100);
                pdf.Pages[0].Save(pathToImage, options);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToImage) { UseShellExecute = true });
        }
    }
}
