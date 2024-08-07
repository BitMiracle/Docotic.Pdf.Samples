using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class TextRenderingMode
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "TextRenderingMode.pdf";

            using (var pdf = new PdfDocument())
            {
                const string sampleText = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                canvas.Pen.Color = new PdfRgbColor(255, 0, 0);
                canvas.Brush.Color = new PdfGrayColor(70);
                canvas.FontSize = 20;
                canvas.Pen.Width = 1;

                canvas.TextRenderingMode = PdfTextRenderingMode.FillAndStroke;
                canvas.DrawString(10, 50, sampleText);

                canvas.TextRenderingMode = PdfTextRenderingMode.Stroke;
                canvas.DrawString(10, 70, sampleText);

                canvas.TextRenderingMode = PdfTextRenderingMode.Fill;
                canvas.DrawString(10, 90, sampleText);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}