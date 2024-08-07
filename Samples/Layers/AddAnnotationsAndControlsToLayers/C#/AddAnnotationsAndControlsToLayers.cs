using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddAnnotationsAndControlsToLayers
    {
        public static void Main()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");

            string pathToFile = "AddAnnotationsAndControlsToLayers.pdf";

            using (var pdf = new PdfDocument())
            {
                pdf.PageMode = PdfPageMode.UseOC;

                PdfLayer buttonsLayer = pdf.CreateLayer("Buttons");
                PdfLayer textLayer = pdf.CreateLayer("Text");

                PdfPage page = pdf.Pages[0];

                int top = 100;
                int height = 50;
                foreach (string name in new string[] { "Button 1", "Button 2", "Button 3" })
                {
                    PdfButton button = page.AddButton(name, new PdfRectangle(10, top, 100, height));
                    button.Text = name;
                    button.Layer = buttonsLayer;

                    top += height + 10;
                }

                top = 100;
                height = 50;
                foreach (string title in new string[] { "Text 1", "Text 2", "Text 3" })
                {
                    PdfTextAnnotation annot = page.AddTextAnnotation(new PdfPoint(200, top), title);
                    annot.Layer = textLayer;
                    top += height + 10;
                }

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");

            Process.Start(new ProcessStartInfo(pathToFile) { UseShellExecute = true });
        }
    }
}
