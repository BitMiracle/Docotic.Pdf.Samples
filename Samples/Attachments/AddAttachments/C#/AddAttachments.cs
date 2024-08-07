using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddAttachments
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "AddAttachments.pdf";

            using (var pdf = new PdfDocument())
            {
                // add shared attachment to the document
                PdfFileSpecification ammerland = pdf.CreateFileAttachment(@"..\Sample Data\ammerland.jpg");
                pdf.SharedAttachments.Add(ammerland);

                // add file attachment annotation to the first page
                var bounds = new PdfRectangle(20, 70, 100, 100);
                PdfFileSpecification jpeg = pdf.CreateFileAttachment(@"..\Sample Data\jpeg.pdf");
                pdf.Pages[0].AddFileAnnotation(bounds, jpeg);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
