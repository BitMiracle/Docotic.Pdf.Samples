using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Comboboxes
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Comboboxes.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                PdfComboBox comboBox = page.AddComboBox(10, 50, 120, 15);
                comboBox.AddItem("First item");
                comboBox.AddItem("Second item");
                comboBox.AddItem("Third item");
                comboBox.Border.Color = new PdfRgbColor(255, 0, 0);
                comboBox.Text = "Second item";

                PdfComboBox comboBox2 = page.AddComboBox(10, 80, 120, 15);
                comboBox2.AddItem("First item");
                comboBox2.AddItem("Second item");
                comboBox2.HasEditField = false;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
