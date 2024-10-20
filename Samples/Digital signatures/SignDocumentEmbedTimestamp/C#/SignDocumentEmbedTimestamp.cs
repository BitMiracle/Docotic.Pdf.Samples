using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SignDocumentEmbedTimestamp
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string outputFileName = "SignDocumentEmbedTimestamp.pdf";
            using (var pdf = new PdfDocument(@"..\Sample Data\jpeg.pdf"))
            {
                // IMPORTANT:
                // Replace "keystore.p12" and "password" with your own .p12 or .pfx path and password.
                // Without the change, the sample will not work.

                var options = new PdfSigningOptions("keystore.p12", "password")
                {
                    DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    Format = PdfSignatureFormat.CadesDetached,
                    Reason = "Testing timestamp embedding",
                    Location = "My workplace",
                    ContactInfo = "support@example.com"
                };

                // Replace the following test URL with your Timestamp Authority URL
                options.Timestamp.AuthorityUrl = new Uri("http://timestamp.digicert.com");

                // Specify username and password if your Timestamp Authority requires authentication
                options.Timestamp.Username = null;
                options.Timestamp.Password = null;

                pdf.SignAndSave(options, outputFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputFileName) { UseShellExecute = true });
        }
    }
}