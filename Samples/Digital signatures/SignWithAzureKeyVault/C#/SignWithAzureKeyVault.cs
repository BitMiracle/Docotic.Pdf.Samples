using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SignWithAzureKeyVault
    {
        public static void Main()
        {
            // IMPORTANT:
            // Change these variables using your vault url, key name, signing algorithm, and Azure credentials.
            const string VaultUrl = "https://YOUR-VAULT.vault.azure.net/";
            const string KeyName = "YOUR-KEY";
            var signingAlgorithm = SignatureAlgorithm.RS512;
            var credentials = new AzureCliCredential();

            var keyClient = new KeyClient(vaultUri: new Uri(VaultUrl), credential: credentials);
            KeyVaultKey key = keyClient.GetKey(KeyName);
            var cryptoClient = new CryptographyClient(key.Id, credentials);
            var signer = new AzureSigner(cryptoClient, signingAlgorithm);

            // This sample automatically generates self-signed certificate for the provided Azure key.
            // In real applications, you might want to use a CA signed certificate.
            using X509Certificate2 cert = AzureCertificateGenerator.Generate(key, cryptoClient, signingAlgorithm);

            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            string outputFileName = "SignWithAzureKeyVault.pdf";
            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];
                PdfSignatureField field = page.AddSignatureField(50, 50, 200, 200);

                var options = new PdfSigningOptions(signer, new[] { cert })
                {
                    DigestAlgorithm = signer.DigestAlgorithm,
                    Field = field,
                };

                pdf.SignAndSave(options, outputFileName);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(outputFileName) { UseShellExecute = true });
        }
    }
}