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

                // By default, the following call uses fonts installed on the operating system.
                // To customize the font source, provide a font loader via PdfConfigurationOptions.
                // For details, see: https://api.docotic.com/pdfconfigurationoptions
                PdfFont systemFont = pdf.CreateFont("Arial", false, true);
                canvas.Font = systemFont;

                var stringOptions = new PdfStringDrawingOptions
                {
                    Strikethrough = true,
                };
                canvas.DrawString(10, 50, "Hello, world!", stringOptions);

                // The following call creates a built-in font. These fonts do not increase file
                // size, as they are included with PDF viewers and don't need to be embedded in the
                // document.
                PdfFont builtInFont = pdf.CreateFont(PdfBuiltInFont.TimesRoman);
                canvas.Font = builtInFont;

                canvas.DrawString(10, 70, "Hello, world!");

                PdfFont fontFromFile = pdf.CreateFontFromFile(@"..\Sample data\Fonts\HolidayPi_BT.ttf");
                canvas.Font = fontFromFile;

                var rect = new PdfRectangle(10, 90, 100, 100);
                var textOptions = new PdfTextDrawingOptions(rect)
                {
                    Underline = true,
                };
                canvas.DrawText("Hello world", textOptions);

                // To ensure consistent appearance across devices, the library embeds all fonts
                // that are not built-in. To reduce output file size, you can remove unused glyphs
                // from the embedded fonts.
                systemFont.RemoveUnusedGlyphs();
                fontFromFile.RemoveUnusedGlyphs();

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}