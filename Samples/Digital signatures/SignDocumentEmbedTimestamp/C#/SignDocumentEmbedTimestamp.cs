using System;
using System.IO;
using System.Reflection;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SignDocumentEmbedTimestamp
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string outputFileName = "SignDocumentEmbedTimestamp.pdf";
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using (PdfDocument pdf = new PdfDocument(Path.Combine(location, "jpeg.pdf")))
            {
                // IMPORTANT:
                // Replace "keystore.p12" and "password" with your own .p12 or .pfx path and password.
                // Without the change the sample will not work.

                PdfSigningOptions options = new PdfSigningOptions("keystore.p12", "password")
                {
                    DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    Format = PdfSignatureFormat.CadesDetached,
                    Reason = "Testing timestamp embedding",
                    Location = "My workplace",
                    ContactInfo = "support@example.com"
                };

                // Replace the following test URL with your Timestamp Authority URL
                options.Timestamp.AuthorityUrl = new Uri("http://tsa.starfieldtech.com/");

                // Specify username and password if your Timestamp Authority requires authentication
                options.Timestamp.Username = null;
                options.Timestamp.Password = null;

                pdf.SignAndSave(options, outputFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}