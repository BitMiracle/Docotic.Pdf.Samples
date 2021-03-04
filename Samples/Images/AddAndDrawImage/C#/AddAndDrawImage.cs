using System.Diagnostics;

namespace BitMiracle.Docotic.Pdf.Samples
{
    public static class AddAndDrawImage
    {
        public static void Main()
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            string pathToFile = "AddAndDrawImage.pdf";

            using (PdfDocument pdf = new PdfDocument())
            {
                PdfCanvas canvas = pdf.Pages[0].Canvas;

                PdfImage image = pdf.AddImage("Sample data/ammerland.jpg");
                canvas.DrawImage(image, 10, 50);

                pdf.Save(pathToFile);
            }

            Process.Start(pathToFile);
        }
    }
}