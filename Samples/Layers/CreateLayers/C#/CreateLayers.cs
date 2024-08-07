using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CreateLayers
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "CreateLayers.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.PageMode = PdfPageMode.UseOC;

                PdfLayer firstLayer = pdf.CreateLayer("First Layer");
                firstLayer.Visible = false;

                PdfLayer secondLayer = pdf.CreateLayer("Second Layer", false, PdfLayerIntent.View);
                secondLayer.Visible = true;

                PdfCanvas canvas = pdf.Pages[0].Canvas;

                canvas.BeginMarkedContent(PdfTagType.OC, firstLayer);
                canvas.DrawString(10, 50, "First");
                canvas.EndMarkedContent();

                canvas.BeginMarkedContent(PdfTagType.OC, secondLayer);
                canvas.DrawString(10, 70, "Second");
                canvas.EndMarkedContent();

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
