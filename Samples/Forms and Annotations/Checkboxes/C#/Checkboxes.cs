using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class Checkboxes
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Checkboxes.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];
                page.AddCheckBox(10, 50, 120, 15, "Check box");

                PdfCheckBox readonlyCheckBox = page.AddCheckBox(10, 100, 120, 15, "Read-only check box");
                readonlyCheckBox.ReadOnly = true;
                readonlyCheckBox.Checked = true;

                PdfCheckBox checkBoxWithBorder = page.AddCheckBox(10, 150, 140, 15, "Check box with colored border");
                checkBoxWithBorder.Border.Color = new PdfCmykColor(40, 90, 70, 3);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
