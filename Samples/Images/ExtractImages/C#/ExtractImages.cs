using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractImages
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var pathToFile = "";

            using (var pdf = new PdfDocument(@"..\Sample Data\gmail-cheat-sheet.pdf"))
            {
                foreach (PdfImage image in pdf.GetImages())
                {
                    pathToFile = image.Save("ExtractedImage");

                    // Only extract first image in this sample
                    break;
                }
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
