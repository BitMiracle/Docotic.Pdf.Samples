using System;
using System.Diagnostics;

using BitMiracle.Docotic.Pdf.PdfA;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CreatePdfA
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "CreatePdfA.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.Pages[0].Canvas.DrawString("Hello PDF/A");
                
                pdf.SaveAsPdfa(pathToFile, PdfaConformanceLevel.Pdfa1A);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
