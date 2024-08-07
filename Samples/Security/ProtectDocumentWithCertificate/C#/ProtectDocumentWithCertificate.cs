using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class ProtectDocumentWithCertificate
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "ProtectDocumentWithCertificate.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.Pages[0].Canvas.DrawString("Hello World!");

                PdfPublicKeyEncryptionHandler handler = CreateEncryptionHandler();
                handler.Algorithm = PdfEncryptionAlgorithm.Aes256Bit;

                var saveOptions = new PdfSaveOptions { EncryptionHandler = handler };
                pdf.Save(pathToFile, saveOptions);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }

        private static PdfPublicKeyEncryptionHandler CreateEncryptionHandler()
        {
            // TODO:
            // Change the constants, or the sample won't work.

            string keyStoreOwner = "key-store-owner.p12";
            string passwordOwner = "password";

            string keyStoreUser = "key-store-user.p12";
            string passwordUser = "password";

            // You can also use an X509Certificate2 certificate to construct the handler
            var handler = new PdfPublicKeyEncryptionHandler(keyStoreOwner, passwordOwner);

            // Add another recipient with non-owner permissions
            var permissions = new PdfPermissions
            {
                Flags = PdfPermissionFlags.FillFormFields | PdfPermissionFlags.PrintDocument
            };
            handler.AddRecipient(keyStoreUser, passwordUser, permissions);

            return handler;
        }
    }
}
