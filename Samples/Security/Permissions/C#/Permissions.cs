using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class Permissions
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "Permissions.pdf";

            using (var pdf = new PdfDocument(@"..\Sample data\form.pdf"))
            {
                // an owner password should be set in order to use user access permissions
                var handler = new PdfStandardEncryptionHandler("owner", "user");
                handler.UserPermissions.Flags = PdfPermissionFlags.None;

                var saveOptions = new PdfSaveOptions { EncryptionHandler = handler };
                pdf.Save(pathToFile, saveOptions);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}