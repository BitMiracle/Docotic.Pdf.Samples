using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Internationalization
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            var pathToFile = "Internationalization.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.FontSize = 12;

                canvas.Font = pdf.CreateFont(PdfBuiltInFont.CourierBold);
                canvas.DrawString(10, 50, "Chinese(traditional): ");
                canvas.DrawString(10, 70, "Russian: ");
                canvas.DrawString(10, 90, "Portugal: ");
                canvas.DrawString(10, 110, "Bidirectional: ");

                canvas.Font = pdf.CreateFont("NSimSun");
                canvas.DrawString(180, 50, "世界您好");

                canvas.Font = pdf.CreateFont("Times New Roman");
                canvas.DrawString(180, 70, "Привет, мир");
                canvas.DrawString(180, 90, "Olá mundo");

                var options = new PdfStringDrawingOptions { ReadingDirection = PdfReadingDirection.Rtl };
                canvas.DrawString(180, 110, "bidi בינלאומי", options);

                pdf.RemoveUnusedFontGlyphs();

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}