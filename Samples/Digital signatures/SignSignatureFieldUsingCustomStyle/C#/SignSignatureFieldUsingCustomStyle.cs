using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SignSignatureFieldUsingCustomStyle
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string outputFileName = "SignSignatureFieldUsingCustomStyle.pdf";
            using (PdfDocument pdf = new PdfDocument("Sample data/SignatureFields.pdf"))
            {
                // IMPORTANT:
                // Replace "keystore.p12" and "password" with your own .p12 or .pfx path and password.
                // Without the change the sample will not work.

                PdfSignatureField field = pdf.GetControl("Control") as PdfSignatureField;
                PdfSigningOptions options = new PdfSigningOptions("keystore.p12", "password")
                {
                    DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    Format = PdfSignatureFormat.Pkcs7Detached,
                    Field = field,
                    Reason = "Testing field styles",
                    Location = "My workplace",
                    ContactInfo = "support@example.com"
                };

                options.Appearance.IncludeDate = false;
                options.Appearance.IncludeDistinguishedName = false;
                
                options.Appearance.NameLabel = "Digital signiert von";
                options.Appearance.ReasonLabel = "Grund:";
                options.Appearance.LocationLabel = "Ort:";

                pdf.SignAndSave(options, outputFileName);
            }

            Process.Start(outputFileName);
        }
    }
}