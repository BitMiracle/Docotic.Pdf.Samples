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
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

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