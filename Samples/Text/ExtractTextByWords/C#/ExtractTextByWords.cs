using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ExtractTextByWords
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

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
