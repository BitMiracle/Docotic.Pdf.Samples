using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Internationalization
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var pathToFile = "Internationalization.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;
                canvas.FontSize = 12;

                canvas.Font = pdf.AddFont(PdfBuiltInFont.CourierBold);
                canvas.DrawString(10, 50, "Chinese(traditional): ");
                canvas.DrawString(10, 70, "Russian: ");
                canvas.DrawString(10, 90, "Portugal: ");
                canvas.DrawString(10, 110, "Bidirectional: ");

                PdfFont? nsimsun = pdf.AddFont("NSimSun");
                if (nsimsun is null)
                {
                    Console.WriteLine("Cannot add NSimSun font");
                    return;
                }

                canvas.Font = nsimsun;
                canvas.DrawString(180, 50, "世界您好");

                PdfFont? timesNewRoman = pdf.AddFont("Times New Roman");
                if (timesNewRoman is null)
                {
                    Console.WriteLine("Cannot add Times New Roman font");
                    return;
                }

                canvas.Font = timesNewRoman;
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