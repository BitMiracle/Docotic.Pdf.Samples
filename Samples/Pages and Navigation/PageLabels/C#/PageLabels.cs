using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class PageLabels
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

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
