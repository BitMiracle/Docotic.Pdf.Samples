using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class OpenCertificateProtectedDocument
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            // TODO:
            // Change the constants, or the sample won't work.
            string encryptedFile = "certificate-encrypted.pdf";
            string keyStore = "key-store.p12";
            string password = "password";

            OpenWithAutoSelectedCertificate(encryptedFile);
            OpenWithKeyStore(encryptedFile, keyStore, password);

            // There are other options. You can use a X509Store or X509Certificate2
            // to construct a PdfPublicKeyDecryptionHandler and then use it 
            // to open a certificate protected document.
        }

        private static void OpenWithAutoSelectedCertificate(string encryptedFile)
        {
            try
            {
                // This will only work if a matching certificate is installed in the
                // X.509 certificate store used by the current user
                using var pdf = new PdfDocument(encryptedFile);
                Console.WriteLine(
                    string.Format(
                        "Opened with an auto-selected certificate from the current user certificate store. " +
                        "Permissions = {0}",
                        pdf.GrantedPermissions
                    )
                );
            }
            catch (PdfException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void OpenWithKeyStore(string encryptedFile, string keyStore, string password)
        {
            try
            {
                var handler = new PdfPublicKeyDecryptionHandler(keyStore, password);
                using var pdf = new PdfDocument(encryptedFile, handler);
                Console.WriteLine(
                    string.Format(
                        "Opened with a certificate from the key store at {0}. Permissions = {1}",
                        keyStore,
                        pdf.GrantedPermissions
                    )
                );
            }
            catch (PdfException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
