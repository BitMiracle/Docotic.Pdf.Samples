using System;
using System.Diagnostics;

using BitMiracle.Docotic.Pdf.Conformance;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ConvertPdfToPdfA
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "ConvertPdfToPdfA.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\gmail-cheat-sheet.pdf"))
                pdf.SaveAsPdfa(pathToFile, PdfaConformanceLevel.Pdfa4);

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
