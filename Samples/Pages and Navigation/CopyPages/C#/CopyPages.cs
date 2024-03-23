using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CopyPages
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            var pathToFile = "CopyPages.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\jfif3.pdf"))
            {
                // copy third and first pages to a new PDF document (page indexes are zero-based)
                using PdfDocument copy = pdf.CopyPages(new int[] { 2, 0 });

                // Helps to reduce file size in cases when the copied pages reference
                // unused resources such as fonts, images, patterns.
                copy.RemoveUnusedResources();

                copy.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
