using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ProtectDocumentWithAes
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "ProtectDocumentWithAes.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                pdf.Pages[0].Canvas.DrawString("Hello World!");

                PdfStandardEncryptionHandler handler = new PdfStandardEncryptionHandler(string.Empty, "user");
                handler.Algorithm = PdfEncryptionAlgorithm.Aes256Bit;
                pdf.SaveOptions.EncryptionHandler = handler;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
