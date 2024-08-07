using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class EmbedFonts
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            const string FilePath = @"..\Sample Data\non-embedded-font.pdf";

            PdfConfigurationOptions config = PdfConfigurationOptions.Create();
            config.FontLoader = new DirectoryFontLoader(new[] { @"..\Sample Data\Fonts" }, false);

            var outputFileName = "EmbedFonts.pdf";

            using (var pdf = new PdfDocument(FilePath, config))
            {
                foreach (PdfFont font in pdf.GetFonts())
                    font.Embed();

                pdf.Save(outputFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputFileName) { UseShellExecute = true });
        }
    }
}