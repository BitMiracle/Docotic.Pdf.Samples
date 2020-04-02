using System.Diagnostics;

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
            using (PdfDocument pdf = new PdfDocument("Sample data/jpeg.pdf"))
            {
                // NOTE:
                // You MUST replace the path and the password parameters passed to
                // the PdfSigningOptions constructor with your own .p12 or .pfx path and password.
                // Otherwise the sample will not work.
                //
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

            Process.Start(outputFileName);
        }
    }
}