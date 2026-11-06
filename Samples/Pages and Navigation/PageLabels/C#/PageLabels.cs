using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class PageLabels
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "PageLabels.pdf";

            using (var pdf = new PdfDocument())
            {
                for (int i = 0; i < 9; i++)
                    pdf.Pages.Add();

                // Use lowercase Roman numerals (i, ii, iii, iv) for the first four pages.
                pdf.PageLabels.Add(0, PdfPageNumberingStyle.LowercaseRoman);

                // Use decimal Arabic numerals (1, 2, 3) for the next three pages.
                pdf.PageLabels.Add(4, PdfPageNumberingStyle.DecimalArabic);

                // Use the labels A-8, A-9, and A-10 for the next three pages.
                pdf.PageLabels.Add(7, PdfPageNumberingStyle.DecimalArabic, "A-", 8);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
