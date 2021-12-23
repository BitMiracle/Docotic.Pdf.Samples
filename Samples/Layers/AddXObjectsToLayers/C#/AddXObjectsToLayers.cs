using System;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddXObjectsToLayers
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.
            
            string pathToFile = "AddXObjectsToLayers.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                pdf.PageMode = PdfPageMode.UseOC;

                PdfLayer firstWatermarkLayer = pdf.CreateLayer("First XObject");
                PdfLayer secondWatermarkLayer = pdf.CreateLayer("Second XObject");

                PdfXObject firstObject = pdf.CreateXObject();
                firstObject.Canvas.DrawString("Text on the first XObject");
                firstObject.Layer = firstWatermarkLayer;

                PdfPage page = pdf.GetPage(0);
                page.Canvas.DrawXObject(firstObject, 0, 100);

                PdfXObject secondObject = pdf.CreateXObject();
                secondObject.Canvas.DrawString("Text on the second XObject");
                secondObject.Layer = secondWatermarkLayer;

                page.Canvas.DrawXObject(secondObject, 100, 200);

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
