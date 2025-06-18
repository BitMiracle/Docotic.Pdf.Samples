using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class FindControlByName
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            const string pathToFile = "FindControlByName.pdf";

            using (var pdf = new PdfDocument(@"..\Sample Data\form.pdf"))
            {
                if (pdf.TryGetControl("email", out var control) &&
                    control is PdfTextBox emailTextBox)
                {
                    emailTextBox.Text = "support@bitmiracle.com";
                }

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
