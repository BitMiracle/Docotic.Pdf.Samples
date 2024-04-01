using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class EmbedFonts
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

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