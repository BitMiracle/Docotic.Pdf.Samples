using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class SignSignatureFieldWithLock
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "SignSignatureFieldWithLock.pdf";

            using (var pdf = new PdfDocument())
            {
                var page = pdf.Pages[0];

                var first = page.AddSignatureField("first", 100, 100, 200, 50);
                // when the first field is signed, fields with "some_text_field" and
                // "some_checkbox" names will be locked for editing
                first.Lock = PdfSignatureFieldLock.CreateLockFields("some_text_field", "some_checkbox");

                var second = page.AddSignatureField("second", 100, 200, 200, 50);
                // when the second field is signed, all fields will be locked for editing
                second.Lock = PdfSignatureFieldLock.CreateLockAll();

                // IMPORTANT:
                // Replace "keystore.p12" and "password" with your own .p12 or .pfx path and password.
                // Without the change the sample will not work.
                var options = new PdfSigningOptions("keystore.p12", "password")
                {
                    DigestAlgorithm = PdfDigestAlgorithm.Sha256,
                    Format = PdfSignatureFormat.Pkcs7Detached,
                    Field = first,
                    Type = PdfSignatureType.AuthorFormFillingAllowed,
                    Reason = "Testing field locking",
                    Location = "My workplace",
                    ContactInfo = "support@example.com"
                };

                pdf.SignAndSave(options, pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
