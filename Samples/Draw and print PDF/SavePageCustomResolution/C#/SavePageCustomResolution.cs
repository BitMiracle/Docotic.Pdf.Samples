using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SavePageCustomResolution
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToImage = "SavePageCustomResolution.png";

            using (var pdf = new PdfDocument(@"..\Sample Data\gmail-cheat-sheet.pdf"))
            {
                PdfDrawOptions options = PdfDrawOptions.Create();
                options.BackgroundColor = new PdfRgbColor(255, 255, 255);
                options.HorizontalResolution = 600;
                options.VerticalResolution = 600;

                pdf.Pages[0].Save(pathToImage, options);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToImage) { UseShellExecute = true });
        }
    }
}
