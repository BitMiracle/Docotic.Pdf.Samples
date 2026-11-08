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
                // Copy the third and first pages, in that order, to a new PDF document (page
                // indexes are zero-based).
                using PdfDocument copy = pdf.Pages.Copy([2, 0]);

                // Reduces the file size by removing unused resources (such as fonts, images, and
                // patterns) that may be copied along with the pages.
                copy.RemoveUnusedResources();

                copy.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
