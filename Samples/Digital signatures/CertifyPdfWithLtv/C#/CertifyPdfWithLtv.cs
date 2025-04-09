using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CertifyPdfWithLtv
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            //LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string outputFileName = "CertifyPdfWithLtv.pdf";
            using (var pdf = new PdfDocument())
            {
                // IMPORTANT:
                // Replace "keystore.p12" and "password" with your own .p12 or .pfx path and password.
                // Without the change, the sample will not work.

                var options = new PdfSigningOptions("keystore.p12", "password")
                {
                    DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    Format = PdfSignatureFormat.CadesDetached,
                    Type = PdfSignatureType.AuthorNoChanges,
                };
                options.Timestamp.AuthorityUrl = new Uri("http://timestamp.digicert.com");

                pdf.AddLtvInfo(options);

                pdf.SignAndSave(options, outputFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputFileName) { UseShellExecute = true });
        }
    }
}