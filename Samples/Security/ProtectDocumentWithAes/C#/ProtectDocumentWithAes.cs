using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ProtectDocumentWithAes
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

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
