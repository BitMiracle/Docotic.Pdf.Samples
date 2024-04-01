using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractTextByWords
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            string pathToFile = "ExtractTextByWords.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                PdfPage page = pdf.Pages[0];
                foreach (PdfTextData data in page.GetWords())
                    page.Canvas.DrawRectangle(data.Bounds);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
