using System;
using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class CreateLayers
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
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
