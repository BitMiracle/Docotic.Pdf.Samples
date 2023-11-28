using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class MovePages
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var continuousFileName = "MovePagesContinuous.pdf";
            var arbitraryFileName = "MovePagesArbitrary.pdf";

            // This shows how to move continuous ranges of pages
            using (var pdf = new PdfDocument())
            {
                BuildTestDocument(pdf);

                // move first half of pages to the end of the document
                pdf.MovePages(0, 5, pdf.PageCount);

                pdf.Save(continuousFileName);
            }

            // This shows how to move arbitrary sets of pages
            using (var pdf = new PdfDocument())
            {
                BuildTestDocument(pdf);

                int[] indexes = new int[] { 0, 2, 4, 6, 8 };

                // move odd pages to the end of the document
                pdf.MovePages(indexes, pdf.PageCount);

                pdf.Save(arbitraryFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(continuousFileName) { UseShellExecute = true });
            Process.Start(new ProcessStartInfo(arbitraryFileName) { UseShellExecute = true });
        }

        private static void BuildTestDocument(PdfDocument pdf)
        {
            PdfFont times = pdf.AddFont(PdfBuiltInFont.HelveticaBoldOblique);
            double pageWidth = pdf.GetPage(0).Width;

            PdfPage page = pdf.GetPage(0);
            for (int i = 0; i < 10; i++)
            {
                if (i > 0)
                    page = pdf.AddPage();

                page.Canvas.Font = times;
                page.Canvas.FontSize = 16;

                string titleFormat = string.Format("Page {0}", i + 1);

                double textWidth = page.Canvas.GetTextWidth(titleFormat);
                page.Canvas.DrawString(new PdfPoint((pageWidth - textWidth) / 2, 100), titleFormat);
            }
        }
    }
}
