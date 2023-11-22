using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddAnnotationsAndControlsToLayers
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
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
