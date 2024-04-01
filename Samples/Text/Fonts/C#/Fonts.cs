using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Fonts
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            string pathToFile = "Fonts.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                PdfFont? systemFont = pdf.AddFont("Arial", false, true, false, true);
                if (systemFont is null)
                {
                    Console.WriteLine("Cannot add font");
                    return;
                }

                canvas.Font = systemFont;
                canvas.DrawString(10, 50, "Hello, world!");

                PdfFont builtInFont = pdf.AddFont(PdfBuiltInFont.TimesRoman);
                canvas.Font = builtInFont;
                canvas.DrawString(10, 70, "Hello, world!");

                PdfFont fontFromFile = pdf.AddFontFromFile(@"..\Sample data\Fonts\HolidayPi_BT.ttf");
                canvas.Font = fontFromFile;
                canvas.DrawString(10, 90, "Hello world");

                // Remove unused glyphs from TrueType fonts to optimize size of result PDF file.
                systemFont.RemoveUnusedGlyphs();
                fontFromFile.RemoveUnusedGlyphs();

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}