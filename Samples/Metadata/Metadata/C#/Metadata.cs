using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Metadata
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Metadata.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.Info.Author = "Sample Browser application";
                pdf.Info.Subject = "Document metadata";
                pdf.Info.Title = "Custom title goes here";
                pdf.Info.Keywords = "pdf Docotic.Pdf";

                PdfPage page = pdf.Pages[0];
                page.Canvas.DrawString(10, 50, "Check document properties");

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
