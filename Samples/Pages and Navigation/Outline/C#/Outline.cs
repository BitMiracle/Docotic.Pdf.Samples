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

                pdf.AddPage();
                pdf.AddPage();

                for (int i = 0; i < pdf.PageCount; ++i)
                {
                    PdfCanvas canvas = pdf.Pages[i].Canvas;
                    canvas.DrawString(260, 50, "Page " + (i + 1).ToString());
                }

                PdfOutlineItem root = pdf.OutlineRoot;
                root.AddChild("Page 1", 0);
                PdfOutlineItem outlineForPage2 = root.AddChild("Page 2", 1);
                outlineForPage2.AddChild("Page 3", 2);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
