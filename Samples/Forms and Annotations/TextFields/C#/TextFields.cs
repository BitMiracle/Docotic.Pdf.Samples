using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class TextFields
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "TextFields.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];
                PdfTextBox textBox = page.AddTextBox(10, 50, 100, 30);
                textBox.Text = "Hello";

                PdfTextBox textBoxWithMaxLength = page.AddTextBox(10, 90, 100, 30);
                textBoxWithMaxLength.MaxLength = 5;

                PdfTextBox passwordTextBox = page.AddTextBox(10, 130, 100, 30);
                passwordTextBox.Text = "Secret password";
                passwordTextBox.PasswordField = true;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
