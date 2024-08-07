using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ReplaceImage
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var pathToFile = "ReplaceImage.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\gmail-cheat-sheet.pdf"))
            {
                foreach (var image in pdf.GetImages(false))
                    image.ReplaceWith(@"..\Sample Data\ammerland.jpg");

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
