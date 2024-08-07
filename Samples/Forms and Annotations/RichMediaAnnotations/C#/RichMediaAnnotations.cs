using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class RichMediaAnnotations
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            const string PathToFile = "RichMediaAnnotations.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];

                var bounds = new PdfRectangle(10, 50, 200, 100);
                PdfFileSpecification sound = pdf.CreateFileAttachment(@"..\Sample Data\sound.mp3");
                PdfRichMediaAnnotation annot = page.AddRichMediaAnnotation(bounds, sound, PdfRichMediaContentType.Sound);
                annot.Activation.ActivationMode = PdfRichMediaActivationMode.OnPageOpen;

                pdf.Save(PathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(PathToFile) { UseShellExecute = true });
        }
    }
}
