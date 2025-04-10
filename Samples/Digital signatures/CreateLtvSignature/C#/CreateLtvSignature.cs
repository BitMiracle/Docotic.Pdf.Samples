using System;
using System.Diagnostics;
using System.IO;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CreateLtvSignature
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string outputFileName = "CreateLtvSignature.pdf";
            using var ms = new MemoryStream();

            // 1. Create regular PAdES B-T signature
            using (var pdf = new PdfDocument())
            {
                // IMPORTANT:
                // Replace "keystore.p12" and "password" with your own .p12 or .pfx path and password.
                // Without the change, the sample will not work.
                var options = new PdfSigningOptions("keystore.p12", "password")
                {
                    DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    Format = PdfSignatureFormat.CadesDetached,
                };
                options.Timestamp.AuthorityUrl = new("http://timestamp.digicert.com");

                pdf.SignAndSave(options, ms);
            }

            // 2. Add LTV information
            using (var pdf = new PdfDocument(ms))
            {
                pdf.AddLtvInfo();

                var incrementalOptions = new PdfSaveOptions()
                {
                    WriteIncrementally = true,
                };
                pdf.Save(outputFileName, incrementalOptions);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputFileName) { UseShellExecute = true });
        }
    }
}