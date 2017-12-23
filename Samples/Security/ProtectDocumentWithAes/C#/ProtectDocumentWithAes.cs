using System.Diagnostics;

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

                pdf.UserPassword = "user";
                pdf.Encryption = PdfEncryptionAlgorithm.Aes256Bit;

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}
