using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SaveAttachment
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            using var pdf = new PdfDocument(@"..\Sample Data\Attachments.pdf");
            PdfFileSpecification? spec = pdf.SharedAttachments["File Attachment testing.doc"];
            if (spec != null && spec.Contents != null)
            {
                const string pathToFile = "attachment.doc";
                spec.Contents.Save(pathToFile);

                Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

                Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
            }
            else
            {
                Console.WriteLine($"Can't save the shared attachment");
            }
        }
    }
}
