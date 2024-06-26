using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class SaveAttachment
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

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
