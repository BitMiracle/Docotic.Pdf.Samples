using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RadioButtons
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "RadioButtons.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                page.Canvas.DrawString(10, 50, "Docotic.Pdf library is written using: ");

                PdfRadioButton radioButton = page.AddRadioButton("languages", 10, 60, 100, 15, "C#");
                radioButton.ExportValue = "csharp";
                radioButton.Checked = true;

                PdfRadioButton radioButton2 = page.AddRadioButton("languages", 10, 80, 100, 15, "Python");
                radioButton2.ExportValue = "python";
                radioButton2.Checked = false;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
