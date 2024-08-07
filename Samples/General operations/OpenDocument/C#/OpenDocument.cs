using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class OpenDocument
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "OpenDocument.pdf";

            using (var pdf = new PdfDocument(@"..\Sample data\jfif3.pdf"))
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.Font = pdf.AddFont(PdfBuiltInFont.Helvetica);
                canvas.FontSize = 20;
                canvas.DrawString(10, 80, "This text was added by Docotic.Pdf");

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}