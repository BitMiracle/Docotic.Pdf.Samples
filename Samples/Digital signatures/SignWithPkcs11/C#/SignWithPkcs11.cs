using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SignWithPkcs11
    {
        public static void Main()
        {
            // IMPORTANT:
            // Change these variables using your PCKS#11 driver, key info and digest algorithm.
            string pkcsLibraryPath = @"C:\Program Files\SoftHSM2\lib\softhsm2-x64.dll";
            ulong slotId = 1743347971;
            string alias = "YOUR-ALIAS";
            string certLabel = "YOUR-CERTIFICATE";
            string pin = "YOUR-PIN";
            var digestAlgorithm = PdfDigestAlgorithm.Sha512;

            using var factory = new Pkcs11SignerFactory(pkcsLibraryPath, slotId);
            using Pkcs11Signer? signer = factory.Load(alias, certLabel, pin, digestAlgorithm);
            if (signer == null)
            {
                Console.WriteLine("Unable to load key. Make sure that the alias and the certificate label are correct.");
                return;
            }

            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            string outputFileName = "SignWithPkcs11.pdf";
            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];
                PdfSignatureField field = page.AddSignatureField(50, 50, 200, 200);

                var options = new PdfSigningOptions(signer, signer.GetChain())
                {
                    DigestAlgorithm = digestAlgorithm,
                    Field = field,
                };

                pdf.SignAndSave(options, outputFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputFileName) { UseShellExecute = true });
        }
    }
}