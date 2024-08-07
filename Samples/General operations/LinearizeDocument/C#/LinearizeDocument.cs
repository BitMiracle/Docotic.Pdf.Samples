using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class LinearizeDocument
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "LinearizeDocument.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\gmail-cheat-sheet.pdf"))
            {
                var saveOptions = new PdfSaveOptions { Linearize = true };
                pdf.Save(pathToFile, saveOptions);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
