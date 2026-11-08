using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class MovePages
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var continuousFileName = "MovePagesContinuous.pdf";
            var arbitraryFileName = "MovePagesArbitrary.pdf";

            // Move a continuous range of pages.
            using (var pdf = new PdfDocument())
            {
                BuildTestDocument(pdf);

                // Move the first five pages to the end of the document.
                pdf.Pages.MoveRange(0, 5, pdf.Pages.Count);

                pdf.Save(continuousFileName);
            }

            // Move pages selected by their indexes.
            using (var pdf = new PdfDocument())
            {
                BuildTestDocument(pdf);

                int[] indexes = [0, 2, 4, 6, 8];

                // Move the pages at the specified indexes to the end of the document.
                pdf.Pages.Move(indexes, pdf.Pages.Count);

                pdf.Save(arbitraryFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(continuousFileName) { UseShellExecute = true });
            Process.Start(new ProcessStartInfo(arbitraryFileName) { UseShellExecute = true });
        }

        private static void BuildTestDocument(PdfDocument pdf)
        {
            PdfFont times = pdf.CreateFont(PdfBuiltInFont.HelveticaBoldOblique);
            double pageWidth = pdf.Pages[0].Width;

            PdfPage page = pdf.Pages[0];
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
