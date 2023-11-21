using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SignSignatureField
    {
        public static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string outputFileName = "SignSignatureField.pdf";
            using (var pdf = new PdfDocument(@"..\Sample Data\SignatureFields.pdf"))
            {
                // IMPORTANT:
                // Replace "keystore.p12" and "password" with your own .p12 or .pfx path and password.
                // Without the change the sample will not work.

                if (pdf.GetControl("Signature2") is not PdfSignatureField field)
                {
                    Console.WriteLine("Cannot find a signature field");
                    return;
                }

                var options = new PdfSigningOptions("keystore.p12", "password")
                {
                    DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    Format = PdfSignatureFormat.Pkcs7Detached,
                    Field = field,
                    Reason = "Testing field signing",
                    Location = "My workplace",
                    ContactInfo = "support@example.com"
                };

                pdf.SignAndSave(options, outputFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputFileName) { UseShellExecute = true });
        }
    }
}