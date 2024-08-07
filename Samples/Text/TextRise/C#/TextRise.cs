using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class TextRise
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "TextRise.pdf";

            using (var pdf = new PdfDocument())
            {
                const string sampleText = "Hello!";
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.TextPosition = new PdfPoint(10, 50);

                canvas.TextRise = 0;
                canvas.DrawString(sampleText);
                canvas.TextRise = 5;
                canvas.DrawString(sampleText);
                canvas.TextRise = -10;
                canvas.DrawString(sampleText);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}