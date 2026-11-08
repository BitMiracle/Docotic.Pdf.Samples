using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddXObjectsToLayers
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "AddXObjectsToLayers.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.PageMode = PdfPageMode.UseOC;

                var layers = pdf.OptionalContent.Layers;
                PdfLayer firstWatermarkLayer = layers.Add("First XObject");
                PdfLayer secondWatermarkLayer = layers.Add("Second XObject");

                PdfXObject firstObject = pdf.CreateXObject();
                firstObject.Canvas.DrawString("Text on the first XObject");
                firstObject.Layer = firstWatermarkLayer;

                PdfPage page = pdf.Pages[0];
                page.Canvas.DrawXObject(firstObject, 0, 100);

                PdfXObject secondObject = pdf.CreateXObject();
                secondObject.Canvas.DrawString("Text on the second XObject");
                secondObject.Layer = secondWatermarkLayer;

                page.Canvas.DrawXObject(secondObject, 100, 200);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
