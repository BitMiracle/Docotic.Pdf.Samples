using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Buttons
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Buttons.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                PdfButton button = page.AddButton(10, 50, 100, 50);
                button.BackgroundColor = new PdfGrayColor(60);
                button.Border.Color = new PdfRgbColor(128, 0, 128);
                button.Text = "Button";

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
