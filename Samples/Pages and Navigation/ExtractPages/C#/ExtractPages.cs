using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractPages
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var extractedPagesFileName = "ExtractPages.pdf";
            var remainingPagesFileName = "ExtractPages_original.pdf";

            using (var original = new PdfDocument())
            {
                for (int i = 0; i < 5; ++i)
                {
                    if (i != 0)
                        original.AddPage();

                    PdfPage page = original.Pages[i];
                    page.Canvas.DrawString("Page #" + (i + 1));
                }

                // extract first 3 pages. These pages will be removed from original PDF document.
                using (PdfDocument extracted = original.ExtractPages(0, 3))
                {
                    // Helps to reduce file size in cases when the extracted pages reference
                    // unused resources such as fonts, images, patterns.
                    extracted.RemoveUnusedResources();

                    extracted.Save(extractedPagesFileName);
                }

                original.Save(remainingPagesFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(extractedPagesFileName) { UseShellExecute = true });
            Process.Start(new ProcessStartInfo(remainingPagesFileName) { UseShellExecute = true });
        }
    }
}
