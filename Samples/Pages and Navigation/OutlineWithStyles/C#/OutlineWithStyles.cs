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

                var outlines = pdf.Outlines;
                var makeBold = true;
                foreach (var child in outlines)
                {
                    if (makeBold)
                        child.Bold = true;
                    else
                        child.Italic = true;
                    makeBold = !makeBold;

                    var makeGreen = true;
                    foreach (var childOfChild in child.Children)
                    {
                        if (makeGreen)
                            childOfChild.Color = new PdfRgbColor(0, 100, 0); // dark green
                        else
                            childOfChild.Color = new PdfRgbColor(138, 43, 226); // blue violet
                        makeGreen = !makeGreen;

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
            PdfOutlineItem? lastParent = null;

            PdfFont times = pdf.CreateFont(PdfBuiltInFont.TimesItalic);
            double pageWidth = pdf.Pages[0].Width;

            PdfPage page = pdf.Pages[pdf.Pages.Count - 1];
            for (int i = 1; i < 30; i++)
            {
                if (i > 1)
                    page = pdf.Pages.Add();

                page.Canvas.Font = times;
                page.Canvas.FontSize = 16;

                string titleFormat = string.Format("Page {0}", i);

                double textWidth = page.Canvas.GetTextWidth(titleFormat);
                page.Canvas.DrawString(new PdfPoint((pageWidth - textWidth) / 2, 100), titleFormat);

                var view = pdf.CreateView(i - 1);

                if (i == 1 || i == 10 || i == 20)
                {
                    lastParent = pdf.Outlines.Add(titleFormat);
                    lastParent.View = view;
                }
                else
                {
                    var child = lastParent!.Children.Add(titleFormat);
                    child.View = view;
                }
            }
        }
    }
}
