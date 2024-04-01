using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SignAlreadySignedDocument
    {
        public static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            // This example won't work without a license. Please get a temporary license and
            // apply it in the next line.
            BitMiracle.Docotic.LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            // IMPORTANT:
            // Please replace
            // * "your-signed-document.pdf" with the path to your file,
            // * "keystore.p12" and "password" with your own .p12 or .pfx path and password.
            // Without the changes the sample will not work.

            string outputFileName = "SignAlreadySignedDocument.pdf";
            using (var pdf = new PdfDocument(@"your-signed-document.pdf"))
            {
                var signingOptions = new PdfSigningOptions("keystore.p12", "password")
                {
                    DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    Format = PdfSignatureFormat.Pkcs7Detached,
                    Reason = "Adding signature to a signed document",
                    Location = "My workplace",
                    ContactInfo = "support@example.com"
                };

                var saveOptions = new PdfSaveOptions
                {
                    // it is extremely important to write the resulting file incrementally
                    // otherwise, the signature in the source file will be invalidated.
                    WriteIncrementally = true,
                };

                pdf.SignAndSave(signingOptions, outputFileName, saveOptions);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputFileName) { UseShellExecute = true });
        }
    }
}