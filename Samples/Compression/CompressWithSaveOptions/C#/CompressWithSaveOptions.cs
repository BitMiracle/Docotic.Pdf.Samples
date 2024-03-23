using System;
using System.Diagnostics;
using System.IO;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CompressWithSaveOptions
    {
        public static void Main()
        {
            string originalFile = @"..\Sample Data\BRAILLE CODES WITH TRANSLATION.pdf";
            const string compressedFile = "CompressWithSaveOptions.pdf";

            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            using (var pdf = new PdfDocument(originalFile))
            {
                var saveOptions = new PdfSaveOptions
                {
                    Compression = PdfCompression.Flate,
                    UseObjectStreams = true,
                    RemoveUnusedObjects = true,
                    OptimizeIndirectObjects = true,
                    WriteWithoutFormatting = true
                };
                pdf.Save(compressedFile, saveOptions);
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
