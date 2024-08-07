using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class SignatureFields
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "SignatureFields.pdf";

            using (var pdf = new PdfDocument())
            {
                var page = pdf.Pages[0];

                // creates a signature field with an auto-generated name
                page.AddSignatureField(100, 100, 200, 50);

                // creates a signature field with the specified name
                page.AddSignatureField("Signature2", 100, 200, 200, 50);

                // creates an invisible signature with the specified name
                // please note the width and height are zero for an invisible signature
                page.AddSignatureField("InvisibleSignature", 350, 100, 0, 0);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
