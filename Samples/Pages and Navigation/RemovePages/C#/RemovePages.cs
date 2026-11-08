using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RemovePages
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var pathToFile = "RemovePages.pdf";
            using (var pdf = new PdfDocument())
            {
                PdfPage firstPage = pdf.Pages[0];
                firstPage.Canvas.DrawString(10, 50, "Main page");

                // Add a page at the end, then remove it.
                pdf.Pages.Add();
                pdf.Pages.RemoveAt(pdf.Pages.Count - 1);

                // Remove multiple pages at once.
                pdf.Pages.Insert(0);
                pdf.Pages.Insert(1);
                pdf.Pages.RemoveAt([0, 1]);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
