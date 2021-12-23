using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SignDocument
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string outputFileName = "SignDocument.pdf";
            using (PdfDocument pdf = new PdfDocument(@"..\Sample Data\jpeg.pdf"))
            {
                // IMPORTANT:
                // Replace "keystore.p12" and "password" with your own .p12 or .pfx path and password.
                // Without the change the sample will not work.

                PdfSigningOptions options = new PdfSigningOptions("keystore.p12", "password")
                {
                    DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    Format = PdfSignatureFormat.Pkcs7Detached,
                    Reason = "Testing digital signatures",
                    Location = "My workplace",
                    ContactInfo = "support@example.com"
                };

                pdf.SignAndSave(options, outputFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}