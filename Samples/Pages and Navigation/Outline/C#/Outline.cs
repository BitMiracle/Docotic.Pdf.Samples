using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Outline
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Outline.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.PageMode = PdfPageMode.UseOutlines;

                pdf.Pages.Add();
                pdf.Pages.Add();

                for (int i = 0; i < pdf.Pages.Count; ++i)
                {
                    PdfCanvas canvas = pdf.Pages[i].Canvas;
                    canvas.DrawString(260, 50, "Page " + (i + 1).ToString());
                }

                var outlines = pdf.Outlines;
                var page1Outline = outlines.Add("Page 1");
                page1Outline.View = pdf.CreateView(0);

                var page2Outline = outlines.Add("Page 2");
                page2Outline.View = pdf.CreateView(1);

                var page3Outline = page2Outline.Children.Add("Page 3");
                page3Outline.View = pdf.CreateView(2);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
