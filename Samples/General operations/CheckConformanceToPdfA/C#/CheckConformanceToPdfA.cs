using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CheckConformanceToPdfA
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            using var pdf = new PdfDocument(@"..\Sample Data\PDF-A.pdf");
            PdfaConformance level = pdf.GetPdfaConformance();
            Console.WriteLine("PDF/A conformance level: " + level);
        }
    }
}
