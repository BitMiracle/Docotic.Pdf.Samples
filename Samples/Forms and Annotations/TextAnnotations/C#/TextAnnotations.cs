using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class TextAnnotations
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "TextAnnotations.pdf";

            using (var pdf = new PdfDocument())
            {
                PdfPage page = pdf.Pages[0];
                const string content = "Lorem ipsum dolor sit amet, consectetur adipisicing elit";
                PdfTextAnnotation textAnnotation = page.AddTextAnnotation(10, 50, "Docotic.Pdf Samples", content);
                textAnnotation.Icon = PdfTextAnnotationIcon.Comment;
                textAnnotation.Opened = true;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
