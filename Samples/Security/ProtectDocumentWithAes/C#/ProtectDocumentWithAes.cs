using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ProtectDocumentWithAes
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            string pathToFile = "ProtectDocumentWithAes.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.Pages[0].Canvas.DrawString("Hello World!");

                var saveOptions = new PdfSaveOptions
                {
                    EncryptionHandler = new PdfStandardEncryptionHandler(string.Empty, "user")
                    {
                        Algorithm = PdfEncryptionAlgorithm.Aes256Bit
                    }
                };
                pdf.Save(pathToFile, saveOptions);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
