using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class OutlineWithStyles
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "OutlineWithStyles.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.PageMode = PdfPageMode.UseOutlines;
                pdf.PageLayout = PdfPageLayout.OneColumn;

                BuildTestOutline(pdf);

                PdfOutlineItem root = pdf.OutlineRoot;

                for (int i = 0; i < root.ChildCount; i++)
                {
                    PdfOutlineItem child = root.GetChild(i);

                    if (i % 2 == 0)
                        child.Bold = true;
                    else
                        child.Italic = true;

                    for (int j = 0; j < child.ChildCount; j++)
                    {
                        PdfOutlineItem childOfChild = child.GetChild(j);

                        if (j % 2 == 0)
                            childOfChild.Color = new PdfRgbColor(0, 100, 0); // dark green
                        else
                            childOfChild.Color = new PdfRgbColor(138, 43, 226); // blue violet

                        childOfChild.Bold = true;
                        childOfChild.Italic = true;
                    }
                }

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static void BuildTestOutline(PdfDocument pdf)
        {
            PdfOutlineItem root = pdf.OutlineRoot;
            PdfOutlineItem? lastParent = null;

            PdfFont times = pdf.CreateFont(PdfBuiltInFont.TimesItalic);
            double pageWidth = pdf.GetPage(0).Width;

            PdfPage page = pdf.GetPage(pdf.PageCount - 1);
            for (int i = 1; i < 30; i++)
            {
                if (i > 1)
                    page = pdf.AddPage();

                page.Canvas.Font = times;
                page.Canvas.FontSize = 16;

                string titleFormat = string.Format("Page {0}", i);

                double textWidth = page.Canvas.GetTextWidth(titleFormat);
                page.Canvas.DrawString(new PdfPoint((pageWidth - textWidth) / 2, 100), titleFormat);

                PdfGoToAction action = pdf.CreateGoToPageAction(i - 1, 0);

                if (i == 1 || i == 10 || i == 20)
                    lastParent = root.AddChild(titleFormat, action);
                else
                    lastParent!.AddChild(titleFormat, action);
            }
        }
    }
}
