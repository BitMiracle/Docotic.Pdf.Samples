using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CopyPages
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var pathToFile = "CopyPages.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\jfif3.pdf"))
            {
                // copy third and first pages to a new PDF document (page indexes are zero-based)
                using PdfDocument copy = pdf.CopyPages(new int[] { 2, 0 });

                // Helps to reduce file size in cases when the copied pages reference
                // unused resources such as fonts, images, patterns.
                copy.RemoveUnusedResources();

                copy.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
