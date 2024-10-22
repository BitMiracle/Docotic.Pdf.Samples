using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RemoveXObjectsWatermarks
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "RemoveXObjectsWatermarks.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\DocumentWithWatermark.pdf"))
            {
                foreach (PdfPage page in pdf.Pages)
                {
                    // Remove the first XObject. Here, it's a watermark.
                    page.XObjects.RemoveAt(0);
                }

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
