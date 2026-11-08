using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SwapPages
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "SwapPages.pdf";

            using (var pdf = new PdfDocument())
            {
                BuildTestDocument(pdf);

                pdf.Pages.Swap(9, 0);
                pdf.Pages.Swap(8, 1);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static void BuildTestDocument(PdfDocument pdf)
        {
            PdfFont times = pdf.CreateFont(PdfBuiltInFont.HelveticaBoldOblique);
            PdfPage page = pdf.Pages[0];
            double pageWidth = page.Width;

            for (int i = 0; i < 10; i++)
            {
                if (i > 0)
                    page = pdf.Pages.Add();

                page.Canvas.Font = times;
                page.Canvas.FontSize = 16;

                string titleFormat = string.Format("Page {0}", i + 1);

                double textWidth = page.Canvas.GetTextWidth(titleFormat);
                page.Canvas.DrawString(new PdfPoint((pageWidth - textWidth) / 2, 100), titleFormat);
            }
        }
    }
}
