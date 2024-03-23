using System;
using System.Diagnostics;
using System.IO;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class FontSubsetting
    {
        public static void Main()
        {
            string originalFile = @"..\Sample Data\gmail-cheat-sheet.pdf";
            const string compressedFile = "FontSubsetting.pdf";

            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            using (var pdf = new PdfDocument(originalFile))
            {
                pdf.RemoveUnusedFontGlyphs();

                pdf.Save(compressedFile);
            }

            // NOTE:
            // This sample shows only one approach to reduce size of a PDF.
            // Please check CompressAllTechniques sample code to see more approaches.
            // https://github.com/BitMiracle/Docotic.Pdf.Samples/tree/master/Samples/Compression/CompressAllTechniques

            string message = string.Format(
                "Original file size: {0} bytes;\r\nCompressed file size: {1} bytes",
                new FileInfo(originalFile).Length,
                new FileInfo(compressedFile).Length
            );
            Console.WriteLine(message);
            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(compressedFile) { UseShellExecute = true });
        }
    }
}
