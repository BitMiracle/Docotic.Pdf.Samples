using System;
using BitMiracle.Docotic.Pdf.Conformance;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ReadPdfaConformance
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            using var pdf = new PdfDocument(@"..\Sample Data\PDF-A.pdf");
            PdfaConformanceLevel? level = pdf.ReadPdfaConformance();
            if (level.HasValue)
                Console.WriteLine("PDF/A conformance level: " + level);
            else
                Console.WriteLine("No PDF/A conformance level");
        }
    }
}
