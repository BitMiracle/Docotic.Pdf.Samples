using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Fonts
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Fonts.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                PdfFont systemFont = pdf.CreateFont("", false, true);
                canvas.Font = systemFont;
                var options = new PdfStringDrawingOptions
                {
                    Strikethrough = true,
                };
                canvas.DrawString(10, 50, "Hello, world!", options);

                PdfFont builtInFont = pdf.CreateFont(PdfBuiltInFont.TimesRoman);
                canvas.Font = builtInFont;
                canvas.DrawString(10, 70, "Hello, world!");

                PdfFont fontFromFile = pdf.CreateFontFromFile(@"..\Sample data\Fonts\HolidayPi_BT.ttf");
                canvas.Font = fontFromFile;
                canvas.DrawString(10, 90, "Hello world");

                // Remove unused glyphs from TrueType fonts to optimize the size of the output.
                systemFont.RemoveUnusedGlyphs();
                fontFromFile.RemoveUnusedGlyphs();

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}