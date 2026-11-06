using System;
using System.Diagnostics;
using System.Linq;

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
                foreach (var page in pdf.Pages)
                {
                    if (page == null)
                        continue;

                    // Remove the first XObject, which is a watermark.
                    var name = page.XObjects.First().Name;
                    page.XObjects.Remove(name);
                }

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
