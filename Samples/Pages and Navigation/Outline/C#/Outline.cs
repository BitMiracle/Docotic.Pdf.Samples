using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Outline
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

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
