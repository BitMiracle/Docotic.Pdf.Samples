using System;

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

            using (PdfDocument pdf = new PdfDocument())
            {
                pdf.PageMode = PdfPageMode.UseOC;

                PdfLayer firstLayer = pdf.CreateLayer("First Layer");
                firstLayer.Visible = false;

                PdfLayer secondLayer = pdf.CreateLayer("Second Layer", false, PdfLayerIntent.View);
                secondLayer.Visible = true;

                pdf.Save(pathToFile);
            }

            Console.WriteLine($"The output is located in {Environment.CurrentDirectory}");
        }
    }
}
