using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class TextFieldAlignment
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            using var pdf = new PdfDocument();
            var startPoint = new PdfPoint(10, 10);
            var size = new PdfSize(100, 100);
            float distance = 30.0f;
            PdfTextAlign[] horizontalAlignments = { PdfTextAlign.Left, PdfTextAlign.Center, PdfTextAlign.Right };

            PdfPage page = pdf.Pages[0];
            for (int h = 0; h < horizontalAlignments.Length; ++h)
            {
                PdfTextBox textBox = page.AddTextBox(
                    startPoint.X + h * (size.Width + distance),
                    startPoint.Y,
                    size.Width,
                    size.Height
                );
                textBox.Multiline = true;
                textBox.TextAlign = horizontalAlignments[h];
                textBox.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum eu ligula ligula, sit amet tempor odio.";
            }

            string pathToFile = "TextFieldAlignment.pdf";
            pdf.Save(pathToFile);

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
