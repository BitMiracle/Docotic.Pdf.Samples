using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractPages
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var extractedPagesFileName = "ExtractPages.pdf";
            var remainingPagesFileName = "ExtractPages_original.pdf";

            using (var original = new PdfDocument())
            {
                for (int i = 0; i < 5; ++i)
                {
                    if (i != 0)
                        original.AddPage();

                    PdfPage page = original.Pages[i];
                    page.Canvas.DrawString("Page #" + (i + 1));
                }

                // Extract the first 3 pages.
                // The call will remove the pages from the original PDF document.
                using (PdfDocument extracted = original.ExtractPages(0, 3))
                {
                    // Helps to reduce file size when the extracted pages reference
                    // unused resources such as fonts, images, patterns.
                    extracted.RemoveUnusedResources();

                    extracted.Save(extractedPagesFileName);
                }

                original.Save(remainingPagesFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(extractedPagesFileName) { UseShellExecute = true });
            Process.Start(new ProcessStartInfo(remainingPagesFileName) { UseShellExecute = true });
        }
    }
}
