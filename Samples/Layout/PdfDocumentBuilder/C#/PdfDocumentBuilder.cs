using System;
using System.Diagnostics;
using BitMiracle.Docotic.Pdf.Layout;

namespace BitMiracle.Docotic.Pdf.Samples
{
    static class PdfDocumentBuilderSample
    {
        static void Main()
        {
            // NOTE:
            // When used in trial mode, the library imposes some restrictions.
            // Please visit https://bitmiracle.com/pdf-library/trial-restrictions
            // for more information.

            const string PathToFile = "PdfDocumentBuilder.pdf";
            PdfDocumentBuilder
                .Create()
                .Encryption(new PdfStandardEncryptionHandler("owner", "user"))
                .Info(info =>
                {
                    info.Author = "Sample author";
                    info.Title = "Sample title";
                })
                .Version(PdfVersion.Pdf16)
                .Generate(PathToFile, doc => doc.Pages(_ => {}));

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
   }
}