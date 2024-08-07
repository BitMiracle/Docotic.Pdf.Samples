using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CreateXObjectFromPage
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "CreateXObjectFromPage.pdf";

            using (var other = new PdfDocument(@"..\Sample Data\jfif3.pdf"))
            {
                using var pdf = new PdfDocument();
                PdfXObject firstXObject = pdf.CreateXObject(other.Pages[0]);
                PdfXObject secondXObject = pdf.CreateXObject(other.Pages[1]);

                PdfPage page = pdf.Pages[0];
                double halfOfPage = page.Width / 2;
                pdf.Pages[0].Canvas.DrawXObject(firstXObject, 0, 0, halfOfPage, 400, 0);
                pdf.Pages[0].Canvas.DrawXObject(secondXObject, halfOfPage, 0, halfOfPage, 400, 0);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
