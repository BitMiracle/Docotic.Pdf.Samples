using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SwapPages
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "SwapPages.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                buildTestDocument(pdf);

                pdf.SwapPages(9, 0);
                pdf.SwapPages(8, 1);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }

        private static void buildTestDocument(PdfDocument pdf)
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
