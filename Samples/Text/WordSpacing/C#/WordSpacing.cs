using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class WordSpacing
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "WordSpacing.pdf";

            using (var pdf = new PdfDocument())
            {
                const string sampleText = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                canvas.WordSpacing = 1;
                canvas.DrawString(10, 50, sampleText);

                canvas.WordSpacing = 4;
                canvas.DrawString(10, 60, sampleText);

                canvas.WordSpacing = 10;
                canvas.DrawString(10, 70, sampleText);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}