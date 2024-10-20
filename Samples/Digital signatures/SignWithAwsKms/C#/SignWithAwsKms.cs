using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SignWithAwsKms
    {
        public static async Task Main()
        {
            // IMPORTANT:
            // Configure AWS credentials in advance (for example, using AWS CLI).
            // And change these variables to use the desired key id and signing algorithm.
            const string KeyId = "arn:aws:kms:YOUR-id";
            var signingAlgorithm = SigningAlgorithmSpec.RSASSA_PSS_SHA_512;

            GetPublicKeyResponse key;
            using (var client = new AmazonKeyManagementServiceClient())
            {
                var getPublicKeyRequest = new GetPublicKeyRequest() { KeyId = KeyId };
                key = await client.GetPublicKeyAsync(getPublicKeyRequest);

                if (!key.SigningAlgorithms.Contains(signingAlgorithm.ToString()))
                {
                    Console.WriteLine($"Unsupported signing algorithm: {signingAlgorithm}");
                    return;
                }
            }

            var signer = new AwsSigner(KeyId, signingAlgorithm);

            // This sample automatically generates self-signed certificate for the provided AWS key.
            // In real applications, you might want to use a CA signed certificate.
            using X509Certificate2 cert = AwsCertificateGenerator.Generate(key, signingAlgorithm);

            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

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