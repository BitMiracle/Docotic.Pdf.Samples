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
                    pdf.AddPage();

                // first four pages will have labels i, ii, iii, and iv
                pdf.PageLabels.AddRange(0, 3, PdfPageNumberingStyle.LowercaseRoman);

                // next three pages will have labels 1, 2, and 3
                pdf.PageLabels.AddRange(4, PdfPageNumberingStyle.DecimalArabic);

                // next three pages will have labels A-8, A-9 and A-10
                pdf.PageLabels.AddRange(7, PdfPageNumberingStyle.DecimalArabic, "A-", 8);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
